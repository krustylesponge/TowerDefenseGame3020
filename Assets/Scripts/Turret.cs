using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab; //will have different types of towerObj that shoot different bullet types, so far i have planned the archer,
                                                      //who has fast but low damage bullets, the wizard, who has slower but stronger bullets, and the axe thrower,
                                                      //who has lower range, but strong bullets (Chris suggested idea to make it more of a melee AoE which i like)
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float attackSpeed = 2;
    [SerializeField] private int baseUpgradeCost = 100;
    [SerializeField] private int maxLevel = 3;

    private float bpsBase; //bps stands for bullets per second
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    //private void OnDrawGizmosSelected() //so we can see the attack range
    //{
    //    Handles.color = Color.yellow;
    //    Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    //}

    private void Start()
    {
        bpsBase = attackSpeed;
        targetingRangeBase = targetingRange;

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
            if (timeUntilFire >= 1/attackSpeed)
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

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.Instance.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.Instance.gold) return;
        if (level >= maxLevel)
        {
            Debug.Log("Max turret level reached");
            return;
        }

        LevelManager.Instance.SpendGold(CalculateCost());

        level++;
        attackSpeed = CalculateBPS();
        targetingRange = CalculateRange();

        CloseUpgradeUI();
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }
}
