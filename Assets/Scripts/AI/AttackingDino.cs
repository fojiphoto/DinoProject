using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingDino : DinoBase
{
    [SerializeField]
    private bool shouldLoopWaypoints = false;
    private bool isWalkingToPlayer = false;

    private void Start()
    {
        base.Start();
        shouldLoopWaypoints = true;
    }

    protected override void OnTargetReached()
    {
        base.OnTargetReached();
        if (isWalkingToPlayer)
        {
            //SetAnimationState(DinoAnimState.BITE);
            SetAnimationTrigger("Bite");
            LevelsManager.Instance.dinosManager.DinoAttackedPlayer(this);
            soundHandler.PlayDinoSound(DinoSound.ATTACK);
            StopDino();
        }
        else if (isLastPointReached())
        {
            if (shouldLoopWaypoints)
            {
                StartMovingToFirstPoint();
            }
            else
            {
                Invoke("WalkTowardsPlayer", 3);
            }
        }
    }

    private void WalkTowardsPlayer()
    {
        isWalkingToPlayer = true;
        SetNavAgentStopingDistance(5);
        SetNavmeshTarget(LevelsManager.Instance.player.transform);
    }

    public override void OnOtherDinoHit()
    {
        shouldLoopWaypoints = false;
        //SetAnimationState(DinoAnimState.BITE);
        SetAnimationTrigger("Bite");
        Invoke("StartMoving", 2.5f);
    }

    private void StartMoving()
    {
        if (waypoints.Length > 0)
        {
            base.SetNavmeshTarget(waypoints[waypoints.Length-1]);
        }
        else
        {
            base.SetNavmeshTarget(LevelsManager.Instance.player.transform);
        }
    }

    protected override void OnDinoBulletHit()
    {
        StopMovingDino();
        Invoke("WalkTowardsPlayer", 4);
        shouldLoopWaypoints = false;
    }
}