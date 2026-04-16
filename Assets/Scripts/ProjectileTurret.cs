using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileTurret : Turret
{
    [SerializeField] private GameObject bulletPrefab; //will have different types of towerObj that shoot different bullet types, so far i have planned the archer,
                                                      //who has fast but low damage bullets, the wizard, who has slower but stronger bullets, and the axe thrower,
                                                      //who has lower range, but strong bullets (Chris suggested idea to make it more of a melee AoE which i like)
    [SerializeField] private Transform firingPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bpsBase = attackSpeed;
        targetingRangeBase = targetingRange;
        damageBase = damage;

        upgradeButton.onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1 / attackSpeed)
            {
                Shoot();
                timeUntilFire = 0;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        bulletScript.SetDamage(damage);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange && target.gameObject.activeSelf; //checks if target is in range AND active
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
