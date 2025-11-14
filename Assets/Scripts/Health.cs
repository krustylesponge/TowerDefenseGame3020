using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitpoints = 2;

    private bool isDead; //helps prevent bugs regarding 2 bullets killing the enemy at the same time
    public void TakeDamage(int damage)
    {
        hitpoints -= damage;

        if (hitpoints <= 0 && !isDead)
        {
            EnemySpawner.onEnemyKill.Invoke();
            isDead = true;
            int LayerDead = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerDead;
            gameObject.SetActive(false);
        }
    }
}
