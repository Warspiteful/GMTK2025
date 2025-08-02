using System;
using UnityEngine;

public class LineHelper : MonoBehaviour
{
    static bool ccw(Vector2 A, Vector2 B, Vector2 C)
    {
        return (C.y - A.y) * (B.x - A.x) > (B.y - A.y) * (C.x - A.x);
    }
    //https://stackoverflow.com/questions/3838329/how-can-i-check-if-two-segments-intersect
    public static bool IsIntersecting(Tuple<Vector2, Vector2> LineA, Tuple<Vector2, Vector2> LineB)
    {
        return (ccw(LineA.Item1, LineB.Item1, LineB.Item2) != ccw(LineA.Item2, LineB.Item1, LineB.Item2)) && ccw(LineA.Item1, LineA.Item2, LineB.Item1) != ccw(LineA.Item1, LineA.Item2, LineB.Item2);
    }
}
