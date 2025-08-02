using System;
using TMPro;
using UnityEngine;

public class Captureable : MonoBehaviour
{
    public int numOfLoopsNeeded = 1;

    public int expGiven = 1;

    private int m_currentNumberOfLoops = 0;

    public Action onCapture;

    private int currentNumberOfLoops
    {
        get
        {
            return m_currentNumberOfLoops;
        }

        set
        {
            m_currentNumberOfLoops = value;

        }
    }

    public bool debugMode;

    [SerializeField] private TextMeshProUGUI debugText;

    void Start()
    {
        debugText.text = (numOfLoopsNeeded - currentNumberOfLoops).ToString();   
    }
    public bool CaptureLoop()
    {
        currentNumberOfLoops++;
        debugText.text = (numOfLoopsNeeded - m_currentNumberOfLoops).ToString();
        if (m_currentNumberOfLoops >= numOfLoopsNeeded)
        {
            return true;
        }

        return false;
    }
}
