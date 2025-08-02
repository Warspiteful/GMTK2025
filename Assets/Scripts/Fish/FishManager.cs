using System;
using System.Collections.Generic;
using UnityEngine;



public class FishManager : MonoBehaviour
{
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
                FishObject obj = Instantiate(fishPrefab);
                obj.Initialize(wave.fish);
            }
        }
    }
}
