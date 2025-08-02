using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer m_renderer;

    public void SetPosition(Vector2 position)
    {
        if (!CanAppend(position)) return;

        m_renderer.positionCount++;
        m_renderer.SetPosition(m_renderer.positionCount - 1, position);
    }

    private bool CanAppend(Vector2 pos)
    {
        if (m_renderer.positionCount == 0) return true;

        return Vector2.Distance(m_renderer.GetPosition(m_renderer.positionCount-1), pos) > DrawManager.RESOLUTION;
    }
}
