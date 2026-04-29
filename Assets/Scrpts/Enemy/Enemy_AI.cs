using System;
using UnityEngine;
using UnityEngine.AI;
using Game.Utils;
using UnityEngine.EventSystems;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 1f;
    [SerializeField] private float roamingTimerMax = 7f;
    [SerializeField] private State startState = State.Roaming;
    [SerializeField] public bool isChasing = false;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float chashingDistance = 7f;
    [SerializeField] private float chasingSpeedMultiplier = 2f;
    [SerializeField] private float attakingDistance = 2f;
    [SerializeField] private float attackRate = 1f;
    private float nextAttackTime = 0f;

    private float walkingSped;
    private float chasingSpeed;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTimer;
    private Vector3 roamPosition;
    private Vector3 startPosition;


    private float nextCheckDirectionTime = 0f;
    private float checkDirectionDuration = 0.1f;
    private Vector3 lastPosition;

    public event EventHandler OnEnemyAttack;
    public bool isRun
    {
        get{
            if (navMeshAgent.velocity == Vector3.zero)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }   

    private enum State
    {
        Idle,
        Roaming,
        Chashing,
        Attacking,
        Death
    }



    private void Awake()
    {
        state = startState;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        walkingSped = navMeshAgent.speed;
        chasingSpeed = navMeshAgent.speed * chasingSpeedMultiplier;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
    }

    public void SetDeathState()
    {
        navMeshAgent.ResetPath();
        state = State.Death;
    }

    private void StateHandler()
    {
        switch (state)
        {
            case State.Roaming:
                roamingTimer -= Time.deltaTime;
                if( roamingTimer < 0)
                {
                    Roaming();
                    roamingTimer = roamingTimerMax;
                }
                SetCurrentState();
                break;
            case State.Idle:
                break;
            case State.Chashing:
                ChashingTarget();
                SetCurrentState();

                break;
            case State.Attacking:
                AttackingTarget();
                SetCurrentState();

                break;
            case State.Death:
                break;
            default:
                break;
        }

    }

    private void SetCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Roaming;
        if (isChasing)
        {
            if(distanceToPlayer <= chashingDistance)
            {
                newState  = State.Chashing;
            }

            if (isAttacking)
            {
                if(distanceToPlayer <= attakingDistance)
                {
                    if (Player.Instance.isAlive())
                        newState = State.Attacking;
                    else
                        newState = State.Roaming;
                }
            }

            if(newState != state)
            {
                if (newState == State.Chashing)
                {
                    navMeshAgent.ResetPath();
                    navMeshAgent.speed = chasingSpeed;
                }
                else if (newState == State.Roaming)
                {
                    roamingTimer = 0f;
                    navMeshAgent.speed = walkingSped;
                }
                else if(newState == State.Attacking)
                {
                    navMeshAgent.ResetPath();

                }
                state = newState;
            }
        }
    }

    private void Roaming()
    {
        startPosition = transform.position;
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
     }

    private void ChashingTarget()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    private void AttackingTarget()
    {
        if (Time.time > nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            nextAttackTime = Time.time + attackRate;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosirion)
    {
        if (sourcePosition.x < targetPosirion.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public float GetCurrnetAnimationSpeedMultiplier()
    {
        return navMeshAgent.speed / walkingSped;
    }


    private void MovementDirectionHandler()
    {
        if(Time.time > nextCheckDirectionTime)
        {
            if (isRun)
            {
                ChangeFacingDirection(lastPosition, transform.position);
            }
            else if (state == State.Attacking)
            {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
            }
            lastPosition = transform.position;
            nextCheckDirectionTime = Time.time + checkDirectionDuration;
        }
    }
}
