using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPrice : MonoBehaviour
{
    public ScriptableBuildings[] buildings;
    private void Awake()
    {
        foreach (ScriptableBuildings building in buildings)
        {

        }
    }

    public void DisplayBuyPrice(float buyPrice)
    {
        foreach (ScriptableBuildings building in buildings)
        {
            building.buildingPriceText.GetComponent<Text>().text = string.Format("PRICE:" + buyPrice);
        }
    }
}
