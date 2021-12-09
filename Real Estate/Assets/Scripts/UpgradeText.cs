using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    public ScriptableBuildings[] buildings;

    private void Awake()
    {

    }
    public void DisplayUpgradePrice(float upgradePrice)
    {
        foreach (ScriptableBuildings building in buildings)
        {
            building.buildingUpgradePriceText.GetComponent<Text>().text = string.Format("PRICE:" + upgradePrice);
        }
    }
}
