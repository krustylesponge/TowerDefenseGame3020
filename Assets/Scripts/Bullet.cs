using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float bulletSpeed = 5f; //different bullet types such as arrows, magic, and axes will have different speeds

    private Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed; //this is so the bullets dont miss the target due to their curvy movement patterns or different speeds 
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //health has yet to be added properly
        gameObject.SetActive(false); //i plan to have a list of bullets to pull from, similarly to enemies
    }
}
