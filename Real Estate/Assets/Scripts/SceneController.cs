using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject tutorial;
    public void TutorialButton()
    {
        if (tutorial.active == false)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    public static void ExitScene()
    {
        Application.Quit();
    }
}
