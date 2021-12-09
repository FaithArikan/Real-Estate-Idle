using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public PlayerData playerData { get; private set; }

    public GameObject scoreText;
    //SPEED CONTROLLER
    public GameObject speedText;
    public float speed2, speed4, speed8, speed12, speed16;
    public GameObject speed2Txt, speed4Txt, speed8Txt, speed12Txt, speed16Txt;
    public Button speed2Btn, speed4Btn, speed8Btn, speed12Btn, speed16Btn;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    private void Start()
    {
        speed2Btn.interactable = false;
        speed4Btn.interactable = false;
        speed8Btn.interactable = false;
        speed12Btn.interactable = false;
        speed16Btn.interactable = false;

        //SPEED TEXTS
        speed2Txt.GetComponent<Text>().text = String.Format("PRICE:" + "${0:#,0}", speed2);
        speed4Txt.GetComponent<Text>().text = String.Format("PRICE:" + "${0:#,0}", speed4);
        speed8Txt.GetComponent<Text>().text = String.Format("PRICE:" + "${0:#,0}", speed8);
        speed12Txt.GetComponent<Text>().text = String.Format("PRICE:" + "${0:#,0}", speed12);
        speed16Txt.GetComponent<Text>().text = String.Format("PRICE:" + "${0:#,0}", speed16);


    }
    void Update()
    {

        speedText.GetComponent<Text>().text = "SPEED:" + PlayerController.Instance.playerSpeed;

        scoreText.GetComponent<Text>().text = String.Format("${0:#,0}", ScoreManager.Instance.score);

        #region interactable
        if (ScoreManager.Instance.score >= speed2)
        {
            speed2Btn.interactable = true;
        }
        if (ScoreManager.Instance.score >= speed4)
        {
            speed4Btn.interactable = true;
        }
        if (ScoreManager.Instance.score >= speed8)
        {
            speed8Btn.interactable = true;
        }
        if (ScoreManager.Instance.score >= speed12)
        {
            speed12Btn.interactable = true;
        }
        if (ScoreManager.Instance.score >= speed16)
        {
            speed16Btn.interactable = true;
        }
        #endregion
        #region destroybtn
        if (PlayerController.Instance.playerSpeed >= 6)
        {
            Destroy(speed2Btn.gameObject);
        }
        if (PlayerController.Instance.playerSpeed >= 12)
        {
            Destroy(speed4Btn.gameObject);
        }
        if (PlayerController.Instance.playerSpeed >= 24)
        {
            Destroy(speed8Btn.gameObject);
        }
        if (PlayerController.Instance.playerSpeed >= 48)
        {
            Destroy(speed12Btn.gameObject);
        }
        if (PlayerController.Instance.playerSpeed >= 96)
        {
            Destroy(speed16Btn.gameObject);
        }
        #endregion
    }
    public void IncreaseSpeedTimes2()
    {
        if (ScoreManager.Instance.score >= speed2)
        {
            ScoreManager.Instance.score = ScoreManager.Instance.score - speed2;
            PlayerController.Instance.playerSpeed = PlayerController.Instance.playerSpeed * 2;
            //BUTTON GO-AWAY
            speed2Btn.interactable = false;
            Destroy(speed2Btn.gameObject);
        }
    }
    public void IncreaseSpeedTimes4()
    {
        if (ScoreManager.Instance.score >= speed4)
        {
            ScoreManager.Instance.score = ScoreManager.Instance.score - speed4;

            PlayerController.Instance.playerSpeed = PlayerController.Instance.playerSpeed * 2;
            //BUTTON GO-AWAY
            speed4Btn.interactable = false;
            Destroy(speed4Btn.gameObject);
        }
    }
    public void IncreaseSpeedTimes8()
    {
        if (ScoreManager.Instance.score >= speed8)
        {
            ScoreManager.Instance.score = ScoreManager.Instance.score - speed8;

            PlayerController.Instance.playerSpeed = PlayerController.Instance.playerSpeed * 2;
            //BUTTON GO-AWAY
            speed8Btn.interactable = false;
            Destroy(speed8Btn.gameObject);
        }
    }
    public void IncreaseSpeedTimes12()
    {
        if (ScoreManager.Instance.score >= speed12)
        {
            ScoreManager.Instance.score = ScoreManager.Instance.score - speed12;

            PlayerController.Instance.playerSpeed = PlayerController.Instance.playerSpeed * 2;
            //BUTTON GO-AWAY
            speed12Btn.interactable = false;
            Destroy(speed12Btn.gameObject);
        }
    }
    public void IncreaseSpeedTimes16()
    {
        if (ScoreManager.Instance.score >= speed16)
        {
            ScoreManager.Instance.score = ScoreManager.Instance.score - speed16;

            PlayerController.Instance.playerSpeed = PlayerController.Instance.playerSpeed * 2;
            //BUTTON GO-AWAY
            speed16Btn.interactable = false;
            Destroy(speed16Btn.gameObject);
        }
    }
    public void OpenIncrease()
    {
        if (speedText.activeSelf == false)
        {
            speedText.SetActive(true);

        }
        else if(speedText.activeSelf == true)
        {
            speedText.SetActive(false);
        }
    }
}
