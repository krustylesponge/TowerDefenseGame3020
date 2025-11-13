using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitpoints = 2;

    public void TakeDamage(int damage)
    {
        hitpoints -= damage;

        if (hitpoints <= 0)
        {
            EnemySpawner.onEnemyKill.Invoke();
            int LayerDead = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerDead;
            gameObject.SetActive(false);
        }
    }
}
