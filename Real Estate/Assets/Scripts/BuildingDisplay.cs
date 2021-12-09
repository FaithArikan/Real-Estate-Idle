using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDisplay : MonoBehaviour
{
    public ScriptableBuildings[] buildings;
    public static BuildingDisplay Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    public void StartTheGame(bool start)
    {
        foreach (ScriptableBuildings building in buildings)
        {
            //PLAYERPREFS
            if(building.rentTimeValue <= 0)
                building.rentTimeValue = building.rentRemainCheck;
            building.bought = PlayerPrefs.GetInt("BoughtBool") == 1 ? true : false;
            building.upgraded = PlayerPrefs.GetInt("UpgradeBool") == 1 ? true : false;

            building.StartTheGame(start);
        }

        if (PlayerController.Instance.playerSpeed == 0)
            PlayerController.Instance.playerSpeed = 3;
        if (ScoreManager.Instance.scoreAdding == 0)
            ScoreManager.Instance.scoreAdding = 1;

    }
    private void OnEnable()
    {
        foreach (ScriptableBuildings building in buildings)
        {
            //CHECKER
            building.timeCheckBuilding = building.rentTimeValue;

        }
    }
    public void RentBuildingTimeManagement(ScriptableBuildings building)
    {
        if (building.buildingRented && building.rentTimeValue > 0)
        {
            building.rentTimeValue -= Time.deltaTime;

            if (building.rentTimeValue <= 0)
            {
                ScoreManager.Instance.scoreAdding -= building.rentScoreBoost;
                building.buildingRented = false;
            }
        }
        if (building.rentTimeValue <= 0)
        {
            //WHEN THE TIMER STOPS, STOPING THE RENT SPEED ACCEL.
            building.buildingRented = false;
            building.rentTimeValue = building.timeCheckBuilding;
        }
    }
    private void Update()
    {
        foreach (ScriptableBuildings building in buildings)
        {
            RentBuildingTimeManagement(building);
            building.OnDisplayTime(building);
            building.BuildingInt();
        }
        PlayerPrefs.Save();
    }

    public void BuyBuilding(ScriptableBuildings building)
    {
        building.OnBuildingBought();
    }
    public void UpgradeBuilding(ScriptableBuildings building)
    {
        building.OnBuildingUpgrade();
    }
    public void RentBuilding(ScriptableBuildings building)
    {
        building.OnRentBuilding();
    }

    private void OnDisable()
    {
        foreach (ScriptableBuildings building in buildings)
        {
            PlayerPrefs.SetInt("BoughtBool", (building.bought ? 1 : 0));
            PlayerPrefs.SetInt("UpgradeBool", (building.upgraded ? 1 : 0));
        }
    }
}
