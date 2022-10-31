using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IAPStorePopup : PopupBase
{
    public Text coinsText, cashText;

    public void ButtonClickEvent(string buttonName)
    {
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        switch (buttonName)
        {
            case "IAPStore":
                {
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
            case "UnlockAll":
                {
                    break;
                }
            case "UnlockAllLevels":
                {
                    break;
                }
            case "UnlockAllGuns":
                {
                    break;
                }
            case "RemoveAds":
                {
                    break;
                }
            default:
                break;
        }
    }

    public override void UpdateUI()
    {
        coinsText.text = PreferenceManager.Coins.ToString();
        cashText.text = PreferenceManager.Cash.ToString();
    }
}