using UnityEngine;

public class RandomMoveable : Moveable
{
    public float movementSpeed = 5f;
    public float movementRadius = 10f;
    public float idleTime = 2f;
    private Vector3 currentTarget;
    private float idleTimer;
    private bool moving;

    void Start()
    {
        idleTimer = idleTime;
        moving = false;
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, movementSpeed * Time.deltaTime);
            if ((transform.position - currentTarget).magnitude < 0.01)
            {
                idleTimer = idleTime;
                moving = false;
            }
        } else
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                SetNewRandomTarget();
                moving = true;
            }
        }
    }

    public Vector3 GetRandomPositionInView()
    {
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        Vector3 viewportPoint = new Vector3(randomX, randomY, 0);

        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(viewportPoint);
        worldPoint.z = transform.position.z;

        return worldPoint;
    }

    void SetNewRandomTarget()
    {
        Vector3 randomPosition = GetRandomPositionInView();
        float distance = (randomPosition - transform.position).magnitude;
        Vector3 direction = (randomPosition - transform.position) / distance;
        float randomRadius = Random.Range(1f, movementRadius);
        float radius = Mathf.Min(distance, randomRadius);
        randomPosition = transform.position + radius * direction;

        currentTarget = randomPosition;
    }
}