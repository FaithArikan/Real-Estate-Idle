using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "Building", menuName = "ScriptableObject", order = 1)]
public class ScriptableBuildings : ScriptableObject
{
    [Header("MAIN")]
    public int spawnNumber;
    [Header("PRICES")]
    public float buildingPrice = 200;
    public float buildingUpgradePrice = 200;
    [Header("BOOST")]
    public float scoreBoost;
    public float upgradeScoreBoost;
    public float rentScoreBoost;
    [Header("RENT TIME")]
    public float rentTimeValue = 20;
    [HideInInspector] public float timeCheckBuilding;
    //TEXT
    [Header("TEXTS")]
    public GameObject rentTimeRemainingText;
    public GameObject buildingPriceText;
    public GameObject buildingUpgradePriceText;
    //BOOL
    [Header("OTHER")]
    public bool bought = false;
    public bool upgraded = false;
    public bool buildingRented = false;
    public float rentRemainCheck;
    //BUTTON
    public GameObject buyButton;
    public GameObject upgradeButton;
    public void StartTheGame(bool start = false)
    {
        foreach (ScriptableBuildings building in BuildingDisplay.Instance.buildings)
        {
            building.timeCheckBuilding = building.rentTimeValue;
            building.rentTimeRemainingText = GameObject.Find("RE" + building.spawnNumber);
            building.buildingUpgradePriceText = GameObject.Find("UP" + building.spawnNumber);
            building.buildingPriceText = GameObject.Find("PR" + building.spawnNumber);
            building.buyButton = GameObject.Find("BuyBtn" + building.spawnNumber);
            building.upgradeButton = GameObject.Find("UpgradeBtn" + building.spawnNumber);

            //BUTTON GETCOMPONENT
            if (buyButton != null)
                buyButton.GetComponent<Button>().interactable = false;
            if (upgradeButton != null)
                upgradeButton.GetComponent<Button>().interactable = false;

            if (building.buildingPriceText == null) 
            { 
                building.bought = true; 
            }
            if (building.buildingUpgradePriceText == null) 
            { 
                building.upgraded = true;
                building.bought = true;
            }
            if (building.rentTimeRemainingText == null)
            {
                building.bought = true;
            }

            //BUY PRICE TEXT
            if (building.buildingPriceText != null)
            building.buildingPriceText.GetComponent<Text>().text = 
                String.Format("PRICE:" + "${0:#,0}" , building.buildingPrice);
            //UPGRADE PRICE TEXT
            if (building.buildingUpgradePriceText != null)
                building.buildingUpgradePriceText.GetComponent<Text>().text =
                String.Format("PRICE:" + "${0:#,0}" , building.buildingUpgradePrice);
            //RENT TEXT
            OnDisplayTime(building);
        }
    }
    public void BuildingInt()
    {
        //BUTTON INT
        if (buildingPrice <= ScoreManager.Instance.score)
        {
            if (buyButton != null)
                buyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            if (buyButton != null)
                buyButton.GetComponent<Button>().interactable = false;
        }
        if (buildingUpgradePrice < ScoreManager.Instance.score && bought)
        {
            if (upgradeButton != null)
                upgradeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            if (upgradeButton != null)
                upgradeButton.GetComponent<Button>().interactable = false;
        }
    }
    public void OnDisplayTime(ScriptableBuildings building)
    {
        float timeToDisplay = building.rentTimeValue;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        building.rentTimeRemainingText.GetComponent<Text>().text
            = string.Format("RENT TIME: {0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    public void OnBuildingBought()
    {
        //BOOST
        ScoreManager.Instance.scoreAdding += scoreBoost;
        //VALUE
        float newMoney = ScoreManager.Instance.score - buildingPrice;
        ScoreManager.Instance.score = newMoney;
        //BOOL
        bought = true;
    }
    public void OnBuildingUpgrade()
    {
        if (bought)
        {
            //BOOST
            ScoreManager.Instance.scoreAdding += upgradeScoreBoost;
            //VALUE
            float newMoney = ScoreManager.Instance.score - buildingUpgradePrice;
            ScoreManager.Instance.score = newMoney;
            upgraded = true;
        }
    }
    public void OnRentBuilding()
    {
        if (bought)
        {
            if (buildingRented == false)
            {
                //BOOST
                rentTimeValue = rentRemainCheck;

                float newRentedScore = ScoreManager.Instance.scoreAdding + rentScoreBoost;
                ScoreManager.Instance.scoreAdding = newRentedScore;
                //BOOL
                buildingRented = true;
                //while time is not 0 boost? -playerprefs is a question
            }
            else
            {
                float newRentedScore = ScoreManager.Instance.scoreAdding - rentScoreBoost;
                ScoreManager.Instance.scoreAdding = newRentedScore;

                buildingRented = false;
                ScoreManager.Instance.score -= rentScoreBoost;
            }
        }
    }

}
