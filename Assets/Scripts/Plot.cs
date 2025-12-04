using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor; //sets plot to yellow when hovered over
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null)
            return; //currently does nothing, will have systems such as selling towers later
        Tower towerToBuild = BuildManager.instance.GetSelectedTower();
        if (!LevelManager.main.SpendGold(towerToBuild.cost)) //checks if we have enough gold to buy tower, if we dont, returns and does nothing, if we do, it goes onward to spawn the tower
        {
            return;
        }
        tower = Instantiate(towerToBuild.towerPrefab, transform.position, Quaternion.identity);
    }
}
