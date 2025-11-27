using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint; //so the enemies know where to spawn from
    public Transform[] path; //so the enemies know where to go

    public int gold;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        gold = 100;
    }

    public void IncreaseGold(int amount)
    {
        gold += amount;
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough gold");
            return false;
        }
    }
}
