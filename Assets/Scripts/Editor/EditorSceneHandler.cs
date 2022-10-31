using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorSceneHandler
{
    private static string splashScenePath = "Assets/Scenes/Splash.unity";
    private static string mainmenuScenePath = "Assets/Scenes/MainMenu.unity";
    private static string forestGameplayScenePath = "Assets/Scenes/GameplayScenes/ForestScene/ForestScene.unity";
    private static string snowGameplayScenePath = "Assets/Scenes/GameplayScenes/SnowScene/SnowScene.unity";
    private static string desertGameplayScenePath = "Assets/Scenes/GameplayScenes/DesertScene/DesertScene.unity";

    [MenuItem("SceneHandler/Open Splash Scene _F1")]
    static void OpenSplashScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(splashScenePath);
        }
    }

    [MenuItem("SceneHandler/Open MainMenu Scene _F3")]
    static void OpenMainMenuScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(mainmenuScenePath);
        }
    }

    [MenuItem("SceneHandler/Open ForestGameplay Scene _F4")]
    static void OpenForestGameplayScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(forestGameplayScenePath);
        }
    }
    [MenuItem("SceneHandler/Open SnowGameplay Scene #F4")]
    static void OpenSnowGameplayScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(snowGameplayScenePath);
        }
    }
    [MenuItem("SceneHandler/Open DesertGameplay Scene %F4")]
    static void OpenDesertGameplayScene()
    {
        if (!EditorApplication.isPlaying && EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(desertGameplayScenePath);
        }
    }

    [MenuItem("SceneHandler/PlayStop _F5")]
    private static void PlayStopButton()
    {
        if (!EditorApplication.isPlaying)
        {
            bool value = EditorApplication.SaveCurrentSceneIfUserWantsTo();
            if (value)
            {
                EditorApplication.OpenScene(splashScenePath);
                EditorApplication.ExecuteMenuItem("Edit/Play");
            }
        }

    }


    [MenuItem("SceneHandler/Pause _F6")]
    private static void PauseButton()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.ExecuteMenuItem("Edit/Pause");
        }
    }
}