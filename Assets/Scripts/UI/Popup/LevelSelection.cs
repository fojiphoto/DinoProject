using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : PopupBase
{
    public GameObject levelsPanel;
    public Text coinsText, cashText;
    private Button[] buttons;

    protected override void Start()
    {
        base.Start();
        buttons = levelsPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < PreferenceManager.UnlockedLevels)
            {
                buttons[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
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
            case "UnlockAllLevels":
                {
                    break;
                }
            default:
                break;
        }
    }

    public void OnLevelButtonClicked(int levelNo)
    {
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        if (PreferenceManager.UnlockedLevels >= levelNo)
        {
            Utilities.currentSelectedLevel = levelNo;

            this._adsManager.Admob_Unity();

            GameManager.Instance.ChangeGameState(GameState.LOADING);
        }
    }

    public override void UpdateUI()
    {
        coinsText.text = PreferenceManager.Coins.ToString();
        cashText.text = PreferenceManager.Cash.ToString();
    }
}