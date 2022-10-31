using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToastHandler : MonoBehaviour
{
    public GameObject toastObject;
    public Text toastText;

    public static ToastHandler Instance;

    private IEnumerator routine;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ShowToast(string text, float startDelay = 2, float stayTime=2)
    {
        if(routine != null)
        {
            StopCoroutine(routine);
        }
        routine = Toast(text, startDelay, stayTime);
        StartCoroutine(routine);
    }

    IEnumerator Toast(string text, float startDelay, float stayTime)
    {
        yield return new WaitForSeconds(startDelay);
        toastText.text = text;
        toastObject.SetActive(true);
        yield return new WaitForSeconds(stayTime);
        toastObject.SetActive(false);
        routine = null;
    }
}