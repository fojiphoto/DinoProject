using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePopup : PopupBase
{
    public GameObject nextButton;

    public Text rewardText, totalText;
    public Text coinsText, cashText;

    public GridLayoutGroup grid;

    private void Start()
    {
        float ratio = (Screen.width + 0.0f) / Screen.height;
        if (ratio > 1.95f)
        {
            grid.cellSize = new Vector2(grid.cellSize.x, 80);
        }

        base.Start();

        if (Utilities.currentSelectedLevel >= Utilities.TOTAL_LEVELS)
        {
            nextButton.SetActive(false);
        }

        this._adsManager.Admob_Unity();
        rewardText.text = 100 + "";
        totalText.text = 100 + "";
    }

    public void OnHomeButtonPressed()
    {
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        Utilities.loadingSceneName = Utilities.MAIN_MENU_SCENE_NAME;
        GameManager.Instance.ChangeGameState(GameState.LOADING);
    }

    public void OnRestartButtonPressed()
    {
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        GameManager.Instance.ChangeGameState(GameState.LOADING);
    }

    public void OnNextButtonPressed()
    {
        Utilities.currentSelectedLevel++;
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        GameManager.Instance.ChangeGameState(GameState.LOADING);
    }

    public void ButtonClickEvent(string buttonName)
    {
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        switch (buttonName)
        {
            case "IAPStore":
                {
                    GameManager.Instance.ChangeGameState(GameState.IAPSTORE);
                    break;
                }
            case "Inventory":
                {
                    GameManager.Instance.ChangeGameState(GameState.INVENTORY);
                    break;
                }
            case "BackBtn":
                {
                    OnBackButtonPressed();
                    break;
                }
            case "Profile":
                {
                    GameManager.Instance.ChangeGameState(GameState.PROFILE);
                    break;
                }
            case "Energy":
                {
                    GameManager.Instance.ChangeGameState(GameState.IAPSTORE);
                    break;
                }
            default:
                break;
        }
    }

    public override void OnBackButtonPressed()
    {
        OnRestartButtonPressed();
    }

    public override void UpdateUI()
    {
        coinsText.text = PreferenceManager.Coins.ToString();
        cashText.text = PreferenceManager.Cash.ToString();
    }
}