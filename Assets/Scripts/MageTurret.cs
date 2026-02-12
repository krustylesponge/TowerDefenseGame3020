using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MageTurret : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float attackSpeed = 2;

    private float timeUntilFire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
