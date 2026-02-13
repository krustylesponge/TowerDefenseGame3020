using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    public static LevelManager main;

    public Transform startPoint; //so the enemies know where to spawn from
    public Transform[] path; //so the enemies know where to go
    
    public int gold;
    public int hp;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        gold = 100;
        hp = 20;
        hpText.text = "HP: " + hp;
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
    
    public void Hurt(int hurtVal) //player takes damage from enemies
    {
        hp -= hurtVal;
        if (hp <= 0)
        {
            hpText.text = "HP: 0";
            EnemySpawner.onDeath.Invoke();
        }
        else
        {
            hpText.text = "HP: " + hp;
        }
    }
}
