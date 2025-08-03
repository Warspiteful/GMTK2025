using System;
using System.Collections.Generic;
using UnityEngine;



public class FishManager : MonoBehaviour
{
    [SerializeField] private FishObjectPool pool;
    public List<FishWave> fishWaves;

    private Queue<FishWave> fishWaveQueue;

    public FishObject fishPrefab;
    FishWave currentWave;

    private int m_numOfFishRemaining;

    private Coroutine m_spawnRoutine;

    public int numOfFish
    {
        get
        {
            return m_numOfFishRemaining;
        }
        set
        {
            m_numOfFishRemaining = value;
            if (m_numOfFishRemaining <= 0)
            {
                HandleWaveFinish();
            }
        }
    }

    private void HandleWaveFinish()
    {
        StartCoroutine(Utils.timer(3, SpawnNextWave));
    }

    public void Start()
    {
        fishWaveQueue = new Queue<FishWave>(fishWaves);
        StartCoroutine(Utils.timer(3, SpawnNextWave));
    }

    public void SpawnNextWave()
    {
        if (fishWaveQueue.Count == 0)
        {
            Debug.Log("Waves Finished");
        }
        else
        {
            currentWave = fishWaveQueue.Dequeue();
            foreach (FishWave.FishSpawn wave in currentWave.fishSpawns)
            {
                for (int i = 0; i < wave.numberOfFish; i++)
                {
                    FishObject obj = pool.GetPooledObject();
                    obj.transform.position = Utils.GetRandomPositionInView();
                    obj.Initialize(wave.fish, ()=>numOfFish--);
                    obj.gameObject.SetActive(true);
                    numOfFish++;
                }
            }
        }
    }
}
