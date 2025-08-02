using System;
using Unity.VisualScripting;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer m_renderer;

    public void SetPosition(Vector2 position)
    {
        if (!CanAppend(position)) return;
        if (CheckOverlap(position))
        {
            Destroy(gameObject);
        }
        m_renderer.positionCount++;
        m_renderer.SetPosition(m_renderer.positionCount - 1, position);
    }

    private bool CanAppend(Vector2 pos)
    {
        if (m_renderer.positionCount == 0) return true;

        return Vector2.Distance(m_renderer.GetPosition(m_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    private bool CheckOverlap(Vector2 position)
    {
        if (m_renderer.positionCount < 1)
        {
            return false;
        }
        Vector2 startPos = m_renderer.GetPosition(0);
        Vector2 lastPos = m_renderer.GetPosition(m_renderer.positionCount-1);
        Tuple<Vector2, Vector2> endLine = new System.Tuple<Vector2, Vector2>(lastPos, position);
        for (int i = 1; i < m_renderer.positionCount - 1; i++)
        {

            Vector2 endPos = m_renderer.GetPosition(i);
            if(LineHelper.IsIntersecting(new System.Tuple<Vector2, Vector2>(startPos, endPos), endLine))
            {
                return true;
            }
            startPos = endPos;
        }
        return false;
    }
}
