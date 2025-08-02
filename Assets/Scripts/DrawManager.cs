using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera m_cam;
    [SerializeField] private Line m__linePrefab;

    public const float RESOLUTION = 0.1f;
    private Line m_currentLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            m_currentLine = Instantiate(m__linePrefab, mousePos, Quaternion.identity);
        }

        if (Input.GetMouseButton(0))
        {
            if (m_currentLine != null)
            {
                m_currentLine.SetPosition(mousePos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_currentLine != null)
            {
                Destroy(m_currentLine.gameObject);
            }
        }
    }
}
