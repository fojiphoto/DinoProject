using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAdUi : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("disableui");
    }
    IEnumerator disableui()
    {
        yield return new WaitForSeconds(0.95f);
        gameObject.SetActive(false);
    }
}
