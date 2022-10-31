using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;
public class PrivacyPolicy : MonoBehaviour
{

    const string PrivacyPolicyLink = "https://docs.google.com/document/d/1Fqo1qwJ4DYsrEb_5_gVyAtpuD636mHEomnMfDO9-mAU/edit";
    public GameObject Game;
    public void Start()
    {
        if (PlayerPrefs.GetInt("PolicyAgreed") == 1)
        {
            OnClickContinueButton();
        }
    }


    public void OnClickQuitButton()
    {

        Application.Quit();
    }


    public void OnClickContinueButton()
    {
        Game.SetActive(true);
        MetaData gdprMetaData = new MetaData("gdpr");
        gdprMetaData.Set("consent", "true");
        Advertisement.SetMetaData(gdprMetaData);
        PlayerPrefs.SetInt("PolicyAgreed", 1);
        gameObject.SetActive(false);
    }
    public void OnClickPrivayButton()
    {
        Application.OpenURL(PrivacyPolicyLink);

    }

}
