using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 2f;

    private Transform target; //tells us where the next place to get to is
    private int pathIndex = 0; //tells us which "node" in the path they've reached

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex >= LevelManager.main.path.Length)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
}
