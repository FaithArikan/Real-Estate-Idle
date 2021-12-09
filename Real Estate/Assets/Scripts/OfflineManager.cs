using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class OfflineManager : MonoBehaviour
{
    public Text goldText;
    public Text timeText;
    public Text offlineprogressText;
    public Text offlineprogressShowText;
    public Text welcomeBackText;

    public int offlineProgressBoost = 20;
    public int offlinePlus = 0;
    void Start()
    {
        int off = PlayerPrefs.GetInt("off");
        offlineProgressBoost = off;
        int off2 = PlayerPrefs.GetInt("offPlus");
        offlinePlus = off2;

        if (PlayerPrefs.HasKey("LAST_LOGIN"))
        {
            DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("LAST_LOGIN"));
            TimeSpan ts = DateTime.Now - lastLogin;
            timeText.text = string.Format("YOU WERE AWAY:<color=#00e1d9> {0} Days {1} Hours {2} Minutes " +
                "{3} Seconds Ago</color>", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            int youAway = (int)ts.TotalSeconds + ((int)ts.TotalSeconds * offlinePlus);
            goldText.text = String.Format("You earned <color=#FFE700>" + "${0:#,0}", youAway + "</color>");

            offlineprogressText.text = String.Format("Price: <color=#F86E9C>" + "${0:#,0}", offlineProgressBoost + "</color>");
            float last = offlinePlus + 1;
            offlineprogressShowText.text = String.Format("Per Second: <color=#F86E9C>" + "${0:#,0}", last + "</color>");
        }
        else
        {
            welcomeBackText.text = "";
            timeText.text = "A game by Ozymandias";
            goldText.text = "Welcome";
        }
    }
    private void Update()
    {
        foreach (ScriptableBuildings building in BuildingDisplay.Instance.buildings)
        {
            if (building.bought)
            {
                Destroy(GameObject.Find("BuyBtn" + building.spawnNumber));
                Destroy(GameObject.Find("PR" + building.spawnNumber));
                Destroy(GameObject.Find("BL" + building.spawnNumber));
            }
            if (building.upgraded)
            {
                Destroy(GameObject.Find("UpgradeBtn" + building.spawnNumber));
                Destroy(GameObject.Find("UP" + building.spawnNumber));
            }
        }
        PlayerPrefs.Save();
    }
    public void OnBuyButtonkill()
    {
        foreach (ScriptableBuildings building in BuildingDisplay.Instance.buildings)
        {
            if (building.bought)
            {
                Destroy(GameObject.Find("BuyBtn" + building.spawnNumber));
                Destroy(GameObject.Find("PR" + building.spawnNumber));
                Destroy(GameObject.Find("BL" + building.spawnNumber));
            }
        }
    }
    public void OnUpgradeButtonkill()
    {
        foreach (ScriptableBuildings building in BuildingDisplay.Instance.buildings)
        {
            if (building.upgraded)
            {
                Destroy(GameObject.Find("UpgradeBtn" + building.spawnNumber));
                Destroy(GameObject.Find("UP" + building.spawnNumber));
            }
        }
    }
    public void OnbuttonPressed()
    {
        DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("LAST_LOGIN"));
        TimeSpan ts = DateTime.Now - lastLogin;

        ScoreManager.Instance.score += (int)ts.TotalSeconds;
    }
    public void OnOfflineProgressButton()
    {
        if (ScoreManager.Instance.score > offlineProgressBoost)
        {        
            //player prefs yap
            float newMoney = ScoreManager.Instance.score - offlineProgressBoost;
            ScoreManager.Instance.score = newMoney;

            offlineProgressBoost += offlineProgressBoost + offlineProgressBoost % 100;
            offlinePlus++;
            //VALUE
            offlineprogressText.text = "Price: <color=#F86E9C>" + offlineProgressBoost + "</color>Dollar";
            float last = offlinePlus + 1;
            offlineprogressShowText.text = "Per Second: <color=#F86E9C>" + last + "</color>Dollar";

            Debug.Log(offlineProgressBoost);
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("off", offlineProgressBoost);
        PlayerPrefs.SetInt("offPlus", offlinePlus);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LAST_LOGIN", DateTime.Now.ToString());
    }
}
