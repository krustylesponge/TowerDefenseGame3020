using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [SerializeField] private GameObject[] towerPrefabs;

    private int selectedTower = 0;

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }
}
