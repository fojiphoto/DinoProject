using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using Google.Play.Review;
#elif UNITY_IOS
using UnityEngine.iOS;
#endif

public class Reveiwrequest : MonoBehaviour
{

    public static Reveiwrequest instance;


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

    public void RequestReview()
    {

#if UNITY_ANDROID

        var reviewManager = new ReviewManager();

        // start preloading the review prompt in the background
        var playReviewInfoAsyncOperation = reviewManager.RequestReviewFlow();

        // define a callback after the preloading is done
        playReviewInfoAsyncOperation.Completed += playReviewInfoAsync =>
        {
            if (playReviewInfoAsync.Error == ReviewErrorCode.NoError)
            {
                // display the review prompt
                var playReviewInfo = playReviewInfoAsync.GetResult();
                reviewManager.LaunchReviewFlow(playReviewInfo);
            }
            else
            {
                // handle error when loading review prompt
            }
        };

#elif UNITY_IOS

        Device.RequestStoreReview();

#endif



    }

}