using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPersistence
{
    public static void SaveData(Interact interact)
    {
        PlayerPrefs.SetFloat("x", interact.transform.position.x);
        PlayerPrefs.SetFloat("y", interact.transform.position.y);
        PlayerPrefs.SetFloat("z", interact.transform.position.z);
    }
    public static ScoreManager LoadData()
    {
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");

        ScoreManager scoreManager = new ScoreManager()
        {
            location = new Vector3(x, y, z),
        };

        return scoreManager;
    }
}
