using System;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer m_renderer;

    Mesh mesh;

    public void SetPosition(Vector2 position)
    {
        if (!CanAppend(position)) return;
        if (CheckOverlap(position))
        {
            MakeMesh();
        }
        m_renderer.positionCount++;
        m_renderer.SetPosition(m_renderer.positionCount - 1, position);
    }

    private bool CanAppend(Vector2 pos)
    {
        if (m_renderer.positionCount == 0) return true;

        return Vector2.Distance(m_renderer.GetPosition(m_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    public void MakeMesh()
    {

        PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
        Vector3[] points = new Vector3[m_renderer.positionCount];
        m_renderer.GetPositions(points);



        // Apply the offset by subtracting the offset (here:  parent's local position) from each vertex position
        for (int i = 0; i < points.Length; i++)
        {
            points[i] -= transform.localPosition;
        }
        Vector2[] pointsAsVector2 = new Vector2[m_renderer.positionCount];
        for (int i = 0; i < m_renderer.positionCount - 1; i++)
    {
            pointsAsVector2[i] = points[i]; 
    }        
        polygonCollider2D.SetPath(0, pointsAsVector2);

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    private bool CheckOverlap(Vector2 position)
    {
        if (m_renderer.positionCount < 1)
        {
            return false;
        }
        Vector2 startPos = m_renderer.GetPosition(0);
        Vector2 lastPos = m_renderer.GetPosition(m_renderer.positionCount - 1);
        Tuple<Vector2, Vector2> endLine = new System.Tuple<Vector2, Vector2>(lastPos, position);
        for (int i = 1; i < m_renderer.positionCount - 1; i++)
        {

            Vector2 endPos = m_renderer.GetPosition(i);
            if (LineHelper.IsIntersecting(new System.Tuple<Vector2, Vector2>(startPos, endPos), endLine))
            {
                return true;
            }
            startPos = endPos;
        }
        return false;
    }
}
