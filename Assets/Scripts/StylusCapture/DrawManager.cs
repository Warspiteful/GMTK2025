using System;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera m_cam;
    [SerializeField] private Line m__linePrefab;


    public Action<int> onHealthChange;

    public Action<int> onExpChange;
    private int m_health = 3;
    private int m_currentExp = 0;

    public int Health
    {
        set
        {
            m_health = value;
            onHealthChange(m_health);
        }
        get
        {
            return m_health;
        }
    }

    public int currentExp
    {
        set
        {
            m_currentExp = value;
            onExpChange(m_currentExp);
        }
        get
        {
            return m_currentExp;
        }
    }
    public const float RESOLUTION = 0.1f;
    private Line m_currentLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_cam = Camera.main;
    }

    private void HandleColliders()
    {
        Debug.Log(m_currentLine.GetColliders().Count);
        foreach (Collider2D collider in m_currentLine.GetColliders())
        {
            if (collider.gameObject.TryGetComponent<Captureable>(out Captureable component))
            {
                if (component.CaptureLoop())
                {
                    currentExp += component.expGiven;
                    Destroy(component.gameObject);
                }
            }
        }

        m_currentLine.GetColliders().Clear();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            m_currentLine = Instantiate(m__linePrefab, mousePos, Quaternion.identity);
        }

        if (m_currentLine != null)
        {



            if (Input.GetMouseButton(0))
            {

                if (m_currentLine.CheckFormLoop(mousePos))
                {
                    HandleColliders();
                    Destroy(m_currentLine.gameObject);
                    m_currentLine = Instantiate(m__linePrefab, mousePos, Quaternion.identity);
                }
                else
                {
                    m_currentLine.SetPosition(mousePos);
                }
            }


            if (m_currentLine.WasCollided())
            {
                Health -= 1;
                Destroy(m_currentLine.gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {

                Destroy(m_currentLine.gameObject);
            }
        }
    }
}
