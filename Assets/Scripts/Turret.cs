using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [SerializeField] protected Transform turretRotationPoint;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] protected Button upgradeButton;

    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected float rotationSpeed = 250f;
    [SerializeField] protected float attackSpeed = 2;
    [SerializeField] private int baseUpgradeCost = 100;
    [SerializeField] private int maxLevel = 3;

    protected float bpsBase; //bps stands for bullets per second
    protected float targetingRangeBase;

    protected Transform target;
    protected float timeUntilFire;

    protected int level = 1;

    //private void OnDrawGizmosSelected() //so we can see the attack range
    //{
    //    Handles.color = Color.yellow;
    //    Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    //}

    private void Start()
    {
        //bpsBase = attackSpeed;
        //Debug.Log(bpsBase);
        //targetingRangeBase = targetingRange;

        //upgradeButton.onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    void Update()
    {

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

        Debug.Log("Upgraded tower stats: atkSpd: " + attackSpeed + ". tgtRng: " + targetingRange + ".");

        CloseUpgradeUI();
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBPS()
    {
        Debug.Log(level);
        Debug.Log(bpsBase);
        Debug.Log(Mathf.Pow(level, 0.6f));
        Debug.Log(bpsBase * Mathf.Pow(level, 0.6f));
        return bpsBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }
}
