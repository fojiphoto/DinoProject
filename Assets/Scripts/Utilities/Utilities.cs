using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static int TOTAL_LEVELS = 6;
    public static float BULLET_HIT_HEALTH_DECREASE = 35;

    public static string loadingSceneName = FOREST_GAMEPLAY_SCENE_NAME;

    public const string MAIN_MENU_SCENE_NAME = "MainMenu";
    public const string FOREST_GAMEPLAY_SCENE_NAME = "ForestScene";
    public const string SNOW_GAMEPLAY_SCENE_NAME = "SnowScene";
    public const string DESERT_GAMEPLAY_SCENE_NAME = "DesertScene";

    public static GameplayMode selectedGameplayMode = GameplayMode.FOREST;
    public static int currentSelectedLevel = 1;

    public static string RateUsUrl
    {
        get
        {
#if UNITY_ANDROID
            return "";
#endif
        }
    }

    public static string MoreGameUrl
    {
        get
        {
#if UNITY_ANDROID
            return "";
#endif
        }
    }
}

[System.Serializable]
public class MenusData
{
    public GameState menuState;
    public string menuName;
}

[System.Serializable]
public class SoundClipData
{
    public SoundClip clipName;
    public AudioClip clip;
}

public enum GameplayMode
{
    FOREST,SNOW,DESERT
}

public enum Tags
{
    Bullets,
    Dino,
    Player
}

public enum DinoAnimState
{
    IDLE = 0,
    WALK = 1,
    RUN = 2,
    BITE = 3,
    DEATH = 4,
}

public enum GameState
{
    MAINMENU,
    SETTINGS,
    LEVELSELECTION,
    GAMEPLAY,
    LEVEL_COMPLETE,
    LEVEL_FAIL,
    GAMEQUIT,
    PAUSED,
    IAPSTORE,
    INVENTORY,
    LOADING,
    PROFILE,
    MODESELECTION,
}

public enum SoundClip
{
    BUTTONCLICK,
    LEVEL_COMPLETE,
    LEVEL_FAILED,
    DINO_KILLED,
    FIRE,
    RELOAD,
    AIM,
    PLAYER_DIED,
    REWARDED,
}

public enum DinoSound
{
    IDLE,
    HURT,
    ATTACK,
    DEATH,
}