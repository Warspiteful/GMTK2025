using System;
using System.Collections.Generic;
using UnityEngine;



public class FishManager : MonoBehaviour
{
    [SerializeField] private FishObjectPool pool;
    public List<FishWave> fishWaves;

    public FishObject fishPrefab;
    FishWave currentWave;
    public void Start()
    {
        if (fishWaves.Count != 0)
        {
            currentWave = fishWaves[0];
        }

        foreach (FishWave.FishSpawn wave in currentWave.fishSpawns)
        {
            for (int i = 0; i < wave.numberOfFish; i++)
            {
                FishObject obj = pool.GetPooledObject();
                obj.transform.position = Utils.GetRandomPositionInView();
                obj.Initialize(wave.fish);
                obj.gameObject.SetActive(true);
            }
        }
    }
}
