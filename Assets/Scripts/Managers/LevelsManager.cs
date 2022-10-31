using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance;
    public HUD hud;

    public GameObject player;
    public DinosManager dinosManager;
    private GameObject level;

    void Start()
    {
        Instance = this;
        level = Instantiate(Resources.Load("Levels/Level_" + Utilities.currentSelectedLevel)) as GameObject;
        dinosManager = level.GetComponent<DinosManager>();
    }

    void OnEnable()
    {
        NeoFPS.FpsGameMode.OnCharacterSpawnedEvent += FpsGameMode_OnCharacterSpawnedEvent;
    }

    void OnDisable()
    {
        NeoFPS.FpsGameMode.OnCharacterSpawnedEvent -= FpsGameMode_OnCharacterSpawnedEvent;
        Instance = null;
    }

    private void FpsGameMode_OnCharacterSpawnedEvent(GameObject character)
    {
        player = character;
    }

    public void OnLevelComplete()
    {
        if (PreferenceManager.UnlockedLevels <= Utilities.currentSelectedLevel)
        {
            ToastHandler.Instance.ShowToast("New Level Unlocked");
            PreferenceManager.UnlockedLevels++;
        }
        PreferenceManager.Coins += 100;
        MenusManager.Instance.UpdateUI();
        hud.SetUIOnLevelEnd();
        Invoke("ShowLevelComplete", 5);
    }

    private void ShowLevelComplete()
    {
        GameManager.Instance.ChangeGameState(GameState.LEVEL_COMPLETE);
    }

    public void OnLevelFailed()
    {
        player.GetComponent<NeoFPS.BasicHealthManager>().AddDamage(120);
        hud.OnPlayerDead();
        PreferenceManager.Coins += 50;
        MenusManager.Instance.UpdateUI();
        Invoke("ShowLevelFailed", 2);
    }

    private void ShowLevelFailed()
    {
        GameManager.Instance.ChangeGameState(GameState.LEVEL_FAIL);
    }
}