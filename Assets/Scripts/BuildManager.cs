using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [SerializeField] private Tower[] towers; //will have more towers than just archer towerObj available

    private int selectedTower = 0; //int for interating through list of towerObj types, will later have a way to change the value

    private void Awake()
    {
        Instance = this;
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
