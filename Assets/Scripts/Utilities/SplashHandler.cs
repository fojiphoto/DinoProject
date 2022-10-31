using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashHandler : MonoBehaviour
{

    void Start()
    {
        Invoke("LoadMenuScene", 2);
    }

    private void LoadMenuScene()
    {
        GameManager.Instance.LoadScene(Utilities.MAIN_MENU_SCENE_NAME);
    }
}