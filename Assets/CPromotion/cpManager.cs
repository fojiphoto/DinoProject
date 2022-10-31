using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class cpManager : MonoBehaviour
{
    public static cpManager instance;

    [System.Serializable]
    public class Links
    {
        public string name;
        public string TextUrl;
        public string ImageUrl;
        public string Link;
        public Texture CpTexture;
    }
    public Links[] _links;

    public bool isAdBackendCall;
    public string AdsStatusLink;
    public string getAdsStatus;
    public string bannerAdLink;
    public string getBannerStatus;

   [HideInInspector] public float AdsVersionID;
   [HideInInspector] public float versionID;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        StartCoroutine(GetTexture());
        StartCoroutine(GetText());

        if(isAdBackendCall)
        {
            StartCoroutine(checkAdsStatus());
            StartCoroutine(checkBannerAds());
            versionID = float.Parse(Application.version);
        }
        
    }

    IEnumerator GetTexture()
    {
        for (int i = 0; i < _links.Length; i++)
        {
            UnityWebRequest loadings = UnityWebRequestTexture.GetTexture(_links[i].ImageUrl);
            yield return loadings.SendWebRequest();
            if (loadings.isDone)
            {
                if (!loadings.isNetworkError)
                {
                    _links[i].CpTexture = ((DownloadHandlerTexture)loadings.downloadHandler).texture;
                }
            }
        }
    }

    IEnumerator GetText()
    {
        for (int i = 0; i < _links.Length; i++)
        {
            UnityWebRequest loadings = UnityWebRequest.Get(_links[i].TextUrl);
            yield return loadings.SendWebRequest();
            if (loadings.isDone)
            {
                if (!loadings.isNetworkError)
                {
                    Debug.Log(loadings.downloadHandler.text);
                    _links[i].Link = loadings.downloadHandler.text;
                }
            }
        }
    }

    IEnumerator checkAdsStatus()
    {

        UnityWebRequest loadings = UnityWebRequest.Get(AdsStatusLink);
        yield return loadings.SendWebRequest();
        if (loadings.isDone)
        {
            Debug.Log(loadings.downloadHandler.text);
            getAdsStatus = loadings.downloadHandler.text;
            AdsVersionID = float.Parse(getAdsStatus);
        }

    }

    IEnumerator checkBannerAds()
    {

        UnityWebRequest loadings = UnityWebRequest.Get(bannerAdLink);
        yield return loadings.SendWebRequest();
        if (loadings.isDone)
        {
            Debug.Log(loadings.downloadHandler.text);
            getBannerStatus = loadings.downloadHandler.text;

        }

    }




}
