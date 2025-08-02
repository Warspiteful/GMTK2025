using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FishWave
{
    [Serializable]
    public struct FishSpawn
    {
        public FishData fish;
        public int numberOfFish;
    }

    [SerializeField] public List<FishSpawn> fishSpawns;
}
