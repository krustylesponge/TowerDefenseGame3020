using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int playerDamage = 1;

    private Transform target; //tells us where the next place to get to is
    private int pathIndex = 0; //tells us which "node" in the path they've reached

    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.Instance.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex >= LevelManager.Instance.path.Length)
            {
                EnemySpawner.onEnemyKill.Invoke();
                LevelManager.Instance.Hurt(playerDamage);
                gameObject.SetActive(false); //i plan to have a list of enemies later that will reuse the enemies that are disabled here to save on memory
                return;
            }
            else
            {
                target = LevelManager.Instance.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed, float timeTillReset) //leave timeTillReset at 0 if its permanent
    {
        moveSpeed = newSpeed;
        if (timeTillReset != 0)
        {
            if (this.gameObject.GetComponent<Health>().GetHitPoints() <= 0)
                return;
            StartCoroutine(ResetSpeed(timeTillReset));
        }
    }

    public IEnumerator ResetSpeed(float freezeTime)
    {
        yield return new WaitForSeconds(freezeTime);
        moveSpeed = baseSpeed; 
    }
}
