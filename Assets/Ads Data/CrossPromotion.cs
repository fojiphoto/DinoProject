using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPromotion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] AdsArray;
    public void CallCrossPromoAd()
    {
        AdsArray[Random.Range(0, 1)].SetActive(true);
    }
}
