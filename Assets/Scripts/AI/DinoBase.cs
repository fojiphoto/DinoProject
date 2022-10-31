using NeoFPS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class DinoBase : MonoBehaviour, IDamageHandler
{
    #region Attributes

    [SerializeField]
    protected DinoSoundsHandler soundHandler;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Collider collider;

    [Space]
    [SerializeField]
    private NavMeshAgent navAgent;
    [SerializeField]
    private float rotationSpeed, runSpeed;
    [SerializeField]
    private float walkSpeed;

    [Space]
    [SerializeField]
    private bool informOthers;
    [SerializeField]
    private float dinoHealthDamageMultiplier = 0.25f;
    [SerializeField]
    private float dinoHealthValue;
    [SerializeField]
    private float hitWaitTime = 4;
    [SerializeField]
    private float minWaitTimeToMove = 1, maxWaitTimeToMove=4;

    public Transform[] waypoints;

    #endregion

    #region Variables

    private DinoAnimState currentAnimState = DinoAnimState.IDLE;
    private bool isBulletHit = false;
    private IEnumerator routine;
    private int currentTarget = 0;
    private Transform currentNavTarget;
    private bool isAlive = true;
    private bool isHit = false;
    private bool isStopped = false;
    private bool isMovingToPlayer = false;

    private float hitWait;

    #endregion

    #region MonoBehaviour
    protected void Start()
    {
        SetNavmeshTarget(waypoints[0]);
    }
    #endregion

    #region Animations
    public void SetAnimationState(DinoAnimState state)
    {
        currentAnimState = state;
        anim.SetInteger("State", (int)state);
    }

    public void SetAnimationTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    #endregion

    #region NavMesh
    public void SetNavmeshTarget(Transform target)
    {
        if (isAlive && !isStopped && navAgent.isActiveAndEnabled)
        {
            currentNavTarget = target;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(currentNavTarget.position, out hit, 1, NavMesh.AllAreas))
            {
                navAgent.SetDestination(hit.position);
                navAgent.isStopped = true;
            }
            if (routine != null)
            {
                StopCoroutine(routine);
            }
            routine = Move();
            StartCoroutine(routine);
        }
    }

    protected void SetNavAgentStopingDistance(float distance)
    {
        navAgent.stoppingDistance = distance;
    }

    IEnumerator Move()
    {
        if (!isMovingToPlayer)
        {
            var waitTime = Random.Range(minWaitTimeToMove, maxWaitTimeToMove);
            yield return new WaitForSeconds(waitTime);
        }
        yield return new WaitForEndOfFrame();
        while (isAlive)
        {
            yield return new WaitForEndOfFrame();
            if (isHit)
            {
                hitWait -= Time.deltaTime;
                if (!navAgent.isStopped)
                {
                    navAgent.isStopped = true;
                }
                if (hitWait <= 0)
                {
                    navAgent.isStopped = false;
                    isHit = false;
                }
            }
            else
            {
                if (!Walk())
                {
                    OnTargetReached();
                    break;
                }
            }
        }
        routine = null;
    }

    private bool Walk()
    {
        if (navAgent.pathStatus != NavMeshPathStatus.PathPartial)
        {
            if(navAgent.isStopped)
                navAgent.isStopped = false;
            if (navAgent.remainingDistance > 8)
            {
                navAgent.speed = runSpeed;
                if (currentAnimState != DinoAnimState.RUN)
                {
                    SetAnimationState(DinoAnimState.RUN);
                }
            }
            else
            {
                navAgent.speed = walkSpeed;
                if (currentAnimState != DinoAnimState.WALK)
                {
                    SetAnimationState(DinoAnimState.WALK);
                }
            }
            if (navAgent.remainingDistance < navAgent.stoppingDistance)
            {
                return false;
            }
        }
        return true;
    }

    protected virtual void OnTargetReached()
    {
        if (isAlive)
        {
            SetAnimationState(DinoAnimState.IDLE);
            soundHandler.PlayDinoSound(DinoSound.IDLE);
            if (!isLastPointReached())
            {
                Invoke("WalkToNextTarget", 2);
            }
        }
    }

    private void WalkToNextTarget()
    {
        if (waypoints.Length > 0 && currentTarget < waypoints.Length)
        {
            SetNavmeshTarget(waypoints[currentTarget]);
        }
        currentTarget ++;
        //currentTarget = (currentTarget + 1) % waypoints.Length;
    }

    protected void StartMovingToFirstPoint()
    {
        currentTarget = 0;
        SetNavmeshTarget(waypoints[currentTarget]);
    }

    public void StopDino(bool isForcedStop = false)
    {
        isStopped = true;
        if (routine != null)
        {
            StopCoroutine(routine);
            navAgent.isStopped = true;
            navAgent.enabled = false;
            var angle = transform.localEulerAngles;
            angle.x = angle.z = 0;
            transform.localEulerAngles = angle;
            collider.enabled = false;
        }
        if (isForcedStop)
        {
            SetAnimationState(DinoAnimState.IDLE);
        }
    }

    protected void StopMovingDino()
    {
        if (isAlive)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }
            routine = null;
            navAgent.isStopped=true;
        }
    }
    #endregion

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (isBulletHit)
        {
            return;
        }
        if (collision.gameObject.CompareTag(Tags.Bullets.ToString()))
        {
            isBulletHit = true;
            SetAnimationState(DinoAnimState.DEATH);
            if (informOthers)
            {
                LevelsManager.Instance.dinosManager.InformOthers(this);
            }
            BehaviourAfterHit();
        }
        else if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            AttackPlayer();
        }
    }

    protected bool isLastPointReached()
    {
        return currentTarget >= waypoints.Length;
    }

    protected void AttackPlayer()
    {
        //SetAnimationState(DinoAnimState.BITE);
            SetAnimationTrigger("Bite");
    }

    private void BehaviourAfterHit()
    {

    }
    #endregion

    #region Others

    public abstract void OnOtherDinoHit();

    #endregion

    #region DamageHandler

    public DamageFilter inDamageFilter { get; set; }

    public DamageResult AddDamage(float damage)
    {
        OnDinoHit(damage);
        return DamageResult.Ignored;
    }

    public DamageResult AddDamage(float damage, RaycastHit hit)
    {
        OnDinoHit(damage);
        return DamageResult.Ignored;
    }

    public DamageResult AddDamage(float damage, IDamageSource source)
    {
        OnDinoHit(damage);
        return DamageResult.Ignored;
    }

    public DamageResult AddDamage(float damage, RaycastHit hit, IDamageSource source)
    {
        OnDinoHit(damage);
        return DamageResult.Ignored;
    }

    private void OnDinoHit(float damage)
    {
        if (isAlive)
        {
            isMovingToPlayer = true;
            hitWait = hitWaitTime;
            soundHandler.PlayDinoSound(DinoSound.HURT);
            isHit = true;
            LevelsManager.Instance.dinosManager.InformOthers(this);
            dinoHealthValue -= (damage * dinoHealthDamageMultiplier);
            LevelsManager.Instance.hud.UpdateDinoFillerValue(dinoHealthValue);
            if (dinoHealthValue <= 0)
            {
                soundHandler.PlayDinoSound(DinoSound.DEATH);
                isAlive = false;
                SetAnimationState(DinoAnimState.DEATH);
                StopDino();
                LevelsManager.Instance.dinosManager.DinoDied(this);
                return;
            }
            else
            {
                //SetAnimationState(DinoAnimState.BITE);
                SetAnimationTrigger("Bite");
                OnDinoBulletHit();
            }
        }
    }

    protected virtual void OnDinoBulletHit()
    {
    }

    #endregion
}