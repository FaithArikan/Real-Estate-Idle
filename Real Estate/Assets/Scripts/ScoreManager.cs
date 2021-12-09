using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public float score = 0;
    public float scoreAdding = 1;
    public float Timer;
    public float DelayAmount = 1; // Second count

    [HideInInspector]
    public Vector3 location;
    public Transform player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
        location = new Vector2(-17, -5.3f);

    }
    private void OnEnable()
    {
        float newScore = PlayerPrefs.GetFloat("Money");
        score = newScore;
        float newScoreAddValue = PlayerPrefs.GetFloat("MoneyPlus");
        scoreAdding = newScoreAddValue;

    }
    private void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= DelayAmount)
        {
            Timer = 0f;
            score += scoreAdding;
        }


        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Money", score);
        PlayerPrefs.SetFloat("MoneyPlus", scoreAdding);
        //PlayerPrefs.DeleteAll();
    }
}
