using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicHandler : MonoBehaviour
{
    public GameObject[] EnableObjectsOnCinematicEnd;
    public float[] EnableObjectsTime;
    public GameObject[] DisbleObjectsOnCinematicEnd;
    public float[] DisableObjectsTime;

    void Start()
    {
        LevelsManager.Instance.hud.DisableHud();

        ToastHandler.Instance.ShowToast("LEVEL TARGET: KILL "+GetComponentInParent<DinosManager>().dinos.Count+" DINASOURS.",2,3);
    }

    public IEnumerator test()
    {
        yield return null;
    }

    public void OnCinematicEnd()
    {
        for (int i = 0; i < EnableObjectsOnCinematicEnd.Length; i++)
        {
            StartCoroutine(EnableDisableObjectWithDelay(EnableObjectsOnCinematicEnd[i], EnableObjectsTime[i], true));
        }
        for (int i = 0; i < DisbleObjectsOnCinematicEnd.Length; i++)
        {
            StartCoroutine(EnableDisableObjectWithDelay(DisbleObjectsOnCinematicEnd[i], DisableObjectsTime[i], false));
        }
    }

    IEnumerator EnableDisableObjectWithDelay(GameObject item, float delay, bool enableObject)
    {
        yield return new WaitForSeconds(delay);
        item.SetActive(enableObject);
    }

    private void OnDisable()
    {
        if (LevelsManager.Instance && LevelsManager.Instance.hud)
        {
            LevelsManager.Instance.hud.EnableHud();
        }
    }
}