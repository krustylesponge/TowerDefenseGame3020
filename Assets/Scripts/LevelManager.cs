using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private int startingGold = 100;
    [SerializeField] private int startingHp = 20;
    [SerializeField] private int levelNumber; 
    [SerializeField] private int nextLevel; //makes it so i can change around levels easier + can check if there is a level to go to, set to -1 for no next level
    public static LevelManager Instance;

    public Transform startPoint; //so the enemies know where to spawn from
    public Transform[] path; //so the enemies know where to go

    public int maxWaveCount;
    public int gold;
    public int hp;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gold = startingGold;
        hp = startingHp;
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

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public int GetNextLevel()
    {
        return nextLevel;
    }
}
