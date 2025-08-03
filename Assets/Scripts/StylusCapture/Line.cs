using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer m_renderer;

    [SerializeField] private int maxPoints;
        
    [SerializeField] PolygonCollider2D captureZonePrefab;

    [SerializeField] EdgeCollider2D edgeCollider2D;

    bool wasCollided = false;

    public bool WasCollided() { return wasCollided;  }

    public void SetPosition(Vector2 position)
    {
        if (!CanAppend(position)) return;

        if (m_renderer.positionCount < maxPoints)
        {
            Debug.Log("MAX: " + maxPoints);
            Debug.Log(m_renderer.positionCount);
            m_renderer.positionCount++;
            m_renderer.SetPosition(m_renderer.positionCount - 1, position);
        }
        else
        {
            Debug.Log("STRETCH: " + m_renderer.positionCount);
            Vector3[] points = new Vector3[m_renderer.positionCount];
            m_renderer.GetPositions(points);
            Vector3[] newPoints = points.Skip(1).Append(position).ToArray();
            m_renderer.SetPositions(newPoints);
        }
        MakeLine();
    }

    public bool CheckFormLoop(Vector2 position)
    {
        if (CheckOverlap(position))
        {
            MakeCaptureMesh();
            return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        wasCollided = true;
    }

    private void MakeLine()
    {
        List<Vector2> pointsAsVector2 = new List<Vector2>();

        for (int i = 0; i < m_renderer.positionCount - 1; i++)
        {
            Vector3 linePoint = m_renderer.GetPosition(i);
            linePoint -= transform.position;
            pointsAsVector2.Add(linePoint);
        }
        
        edgeCollider2D.SetPoints(pointsAsVector2);
    }

    private bool CanAppend(Vector2 pos)
    {
        if (m_renderer.positionCount == 0) return true;

        return Vector2.Distance(m_renderer.GetPosition(m_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    public void MakeCaptureMesh()
    {

        Destroy(edgeCollider2D);
        PolygonCollider2D polygonCollider2D = Instantiate(captureZonePrefab, transform.position, Quaternion.identity, transform);
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

        List<Collider2D> colliderList = new List<Collider2D>();
        Physics2D.OverlapCollider(polygonCollider2D, colliderList);
        captureColliders = colliderList.ToHashSet();
    }

    
     private HashSet<Collider2D> captureColliders = new HashSet<Collider2D>();

     public HashSet<Collider2D> GetColliders () { return captureColliders; }

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
