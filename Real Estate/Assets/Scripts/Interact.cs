using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public ScoreManager scoreManager { get; private set; }

    private void OnEnable()
    {
        scoreManager = PlayerPersistence.LoadData();

        transform.position = scoreManager.location;

    }
    private void OnDisable()
    {
        PlayerPersistence.SaveData(this);
    }
}
