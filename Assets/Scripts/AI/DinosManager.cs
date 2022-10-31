using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosManager : MonoBehaviour
{
    public GameObject endCinematicCam;

    public List<DinoBase> dinos;

    private bool hasInformedOtherDino = false, hasLevelEnded;

    void Start()
    {
        LevelsManager.Instance.hud.UpdateDinoTargetCount(dinos.Count);
    }

    public void InformOthers(DinoBase dino)
    {
        if (hasInformedOtherDino)
        {
            return;
        }
        hasInformedOtherDino = true;
        foreach (var d in dinos)
        {
            if(d != dino)
            {
                d.OnOtherDinoHit();
            }
        }
    }

    public void DinoAttackedPlayer(DinoBase dino)
    {
        if (!hasLevelEnded)
        {
            hasLevelEnded = true;

            foreach (var d in dinos)
            {
                if (d != dino)
                {
                    d.StopDino(true);
                }
            }

            LevelsManager.Instance.hud.SetUIOnLevelEnd();
            Invoke("CallLevelFailed", 1);
        }
    }

    private void CallLevelFailed()
    {
        LevelsManager.Instance.OnLevelFailed();
    }

    public void DinoDied(DinoBase dino)
    {
        if (hasLevelEnded)
        {
            return;
        }
        if (dinos.Contains(dino))
        {
            dinos.Remove(dino);
        }
        LevelsManager.Instance.hud.UpdateDinoTargetCount(dinos.Count);
        if(dinos.Count == 0)
        {
            hasLevelEnded = true;
            endCinematicCam.GetComponent<EndCameraHandler>().SetTarget(dino.transform);
            endCinematicCam.SetActive(true);
            LevelsManager.Instance.OnLevelComplete();
        }
    }
}