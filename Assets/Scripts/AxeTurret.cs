using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AxeTurret : Turret
{
    [SerializeField] Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bpsBase = attackSpeed;
        Debug.Log(bpsBase);
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1 / attackSpeed)
        {
            Slice();
            timeUntilFire = 0;
        }
    }
    private void Slice()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            anim.SetTrigger("SeenEnemy");
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                hit.transform.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
