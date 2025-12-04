using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldUI;

    private void OnGUI()
    {
        goldUI.text = "Gold: " + LevelManager.main.gold.ToString(); //makes the gold value able to be placed into the gold for the shop
    }

    public void SetSelected()
    {

    }
}
