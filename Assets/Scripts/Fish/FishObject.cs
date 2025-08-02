using UnityEngine;

public class FishObject : MonoBehaviour
{
    [SerializeField] private Captureable captureable;

    [SerializeField] private Moveable moveable;

    public void Initialize(FishData fishData)
    {
        captureable.expGiven = fishData.expGiven;
        captureable.numOfLoopsNeeded = fishData.numOfLoopsNeeded;
    }
}
