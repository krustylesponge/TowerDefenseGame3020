using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float bulletSpeed = 5f; //different bullet types such as arrows, magic, and axes will have different speeds and damages
    [SerializeField] bool bulletCausesSlowness = false;
    [SerializeField] float bulletSlowness = 0.5f;
    [SerializeField] float slownessTime = 2;

    private int bulletDamage = 0;

    private Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target || (transform.position - target.transform.position).sqrMagnitude < 0.01f)
        {
            gameObject.SetActive(false); //stops bullet from lingering on track
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed; //this is so the bullets dont miss the target due to their curvy movement patterns or different speeds 
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(int _damage)
    {
        bulletDamage = _damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        if (bulletCausesSlowness)
        {
            EnemyMovement em = other.transform.GetComponent<EnemyMovement>();
            em.UpdateSpeed(bulletSlowness, slownessTime);
        }
        gameObject.SetActive(false); //i plan to have a list of bullets to pull from, similarly to enemies
    }
}
