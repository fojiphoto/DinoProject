using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PreferenceManager
{
    #region Save

    public static void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    #endregion

    public static float touchpadSensitivity
    {
        get
        {
            return PlayerPrefs.GetFloat("TOUCHPAD_SENSITIVITY", 0.5f);
        }
        set
        {
            PlayerPrefs.SetFloat("TOUCHPAD_SENSITIVITY", value);
        }
    }

    public static int GraphicSettings
    {
        get
        {
            return PlayerPrefs.GetInt("GRAPHIC_SETTINGS", 2);
        }
        set
        {
            PlayerPrefs.SetInt("GRAPHIC_SETTINGS", value);
        }
    }

    public static int Coins
    {
        get
        {
            return PlayerPrefs.GetInt("COINS", 0);
        }
        set
        {
            PlayerPrefs.SetInt("COINS", value);
        }
    }

    public static int Cash
    {
        get
        {
            return PlayerPrefs.GetInt("CASH", 0);
        }
        set
        {
            PlayerPrefs.SetInt("CASH", value);
        }
    }

    public static int UnlockedLevels
    {
        get
        {
            return PlayerPrefs.GetInt("Levels", 1);
        }
        set
        {
            PlayerPrefs.SetInt("Levels", value);
        }
    }

    public static bool isSoundOn
    {
        get
        {
            return PlayerPrefs.GetInt("SOUND", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("SOUND", value?1:0);
        }
    }

    public static bool isMusicOn
    {
        get
        {
            return PlayerPrefs.GetInt("MUSIC", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("MUSIC", value ? 1 : 0);
        }
    }

    public static bool GetModeStatus(GameplayMode mode)
    {
        return PlayerPrefs.GetInt("MODE:"+ mode, 0) == 1;
    }
    public static void SetModeStatus(GameplayMode mode,bool value)
    {
        PlayerPrefs.SetInt("MODE:" + mode, value?1:0);
    }
}