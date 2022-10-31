using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPopup : PopupBase
{
    [SerializeField]
    private Text coinsText, cashText;

    private void Start()
    {
        base.Start();
        MenusManager.Instance.HudPopup = this;
    }

    public override void UpdateUI()
    {
        coinsText.text = PreferenceManager.Coins.ToString();
        cashText.text = PreferenceManager.Cash.ToString();
    }

    private void OnDestroy()
    {
        MenusManager.Instance.HudPopup = null;
    }

    public override void OnBackButtonPressed()
    {
        GameManager.Instance.ChangeGameState(GameState.PAUSED);
    }
}