using UnityEngine;

public class Utils
{
    public static Vector2 GetRandomPositionInView()
    {
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        Vector3 viewportPoint = new Vector3(randomX, randomY, 0);

        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(viewportPoint);

        return worldPoint;
    }
}