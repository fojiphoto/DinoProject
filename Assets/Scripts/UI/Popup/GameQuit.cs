using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : PopupBase
{
    public void OnButtonPressed(string btn)
    {
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        switch (btn)
        {
            case "Yes":
                {
                    Application.Quit();
                    break;
                }
            case "No":
                {
                    OnBackButtonPressed();
                    break;
                }
        }
    }

    public override void UpdateUI()
    {
    }
}