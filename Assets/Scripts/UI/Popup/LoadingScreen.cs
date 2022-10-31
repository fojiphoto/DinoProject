using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : PopupBase
{
    public Image fillerImage;

    private float fillAmountMultiplier = 1f;
    private AsyncOperation operation;

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(this.gameObject);
        fillerImage.fillAmount = 0;
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2);
        operation = GameManager.Instance.LoadScene(Utilities.loadingSceneName);
        while (fillerImage.fillAmount < 1)
        {
            fillerImage.fillAmount += Time.deltaTime*fillAmountMultiplier;
            if (operation.isDone && fillAmountMultiplier < 2)
            {
                fillAmountMultiplier = 50;
            }
            yield return new WaitForEndOfFrame();
        }
        fillerImage.fillAmount = 1;
        yield return new WaitForSeconds(2);
        if (Utilities.loadingSceneName.Equals(Utilities.FOREST_GAMEPLAY_SCENE_NAME) ||
            Utilities.loadingSceneName.Equals(Utilities.SNOW_GAMEPLAY_SCENE_NAME) ||
            Utilities.loadingSceneName.Equals(Utilities.DESERT_GAMEPLAY_SCENE_NAME))
        {
            GameManager.Instance.ChangeGameState(GameState.GAMEPLAY);
        }
        else if (Utilities.loadingSceneName == Utilities.MAIN_MENU_SCENE_NAME)
        {
            GameManager.Instance.ChangeGameState(GameState.MAINMENU);
        }
        Destroy(gameObject);
    }

    public override void UpdateUI()
    {
    }
}