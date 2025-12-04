using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [SerializeField] private Tower[] towers; //will have more towers than just archer tower available

    private int selectedTower = 0; //int for interating through list of tower types, will later have a way to change the value

    private void Awake()
    {
        instance = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
