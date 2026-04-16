using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AxeTurret : Turret
{
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
        //if (target == null)
        //{
        //    FindTarget();
        //    return;
        //}
        //RotateTowardsTarget();
        //if (!CheckTargetIsInRange())
        //{
        //    target = null;
        //}
        //else
        //{
        //    timeUntilFire += Time.deltaTime;
        //    if (timeUntilFire >= 1 / attackSpeed)
        //    {
        //        Shoot();
        //        timeUntilFire = 0;
        //    }
        //}
    }
}
