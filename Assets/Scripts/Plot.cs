using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    public GameObject towerObj;
    public Turret turret;
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
        if (UIManager.Instance.IsHoveringUI())
            return;

        if (towerObj != null)
        {
            turret.OpenUpgradeUI(); 
            return;
        }
        Tower towerToBuild = BuildManager.Instance.GetSelectedTower();
        if (!LevelManager.Instance.SpendGold(towerToBuild.cost)) //checks if we have enough gold to buy towerObj, if we dont, returns and does nothing, if we do, it goes onward to spawn the towerObj
        {
            return;
        }
        towerObj = Instantiate(towerToBuild.towerPrefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }
}
