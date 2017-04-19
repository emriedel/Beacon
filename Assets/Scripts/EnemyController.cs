using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public enum State { WANDER, CHASE };
    public UnityEngine.AI.NavMeshAgent Nav;
    public float CurrentSpeed;
    public float PatrolWaitTime = 1f;

    public State CurrentState;
    private EnemySight Sight;

    //wandering variables
    public float WanderSpeed = 3.0f;
    public GameObject[] PatrolPoints;

    private int PatrolPointIndex;
    private float PatrolTimer;

    //chasing variables
    public float ChaseSpeed = 7.0f;
    public float AlertWaitTime = 5.0f;
    public GameObject Player;

    private float AlertTimer;


    void Awake () {
        Nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //PatrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        Sight = GetComponentInChildren<EnemySight>();
        Player = GameObject.FindGameObjectWithTag("Player");
        CurrentState = State.WANDER;
        CurrentSpeed = WanderSpeed;
	}
	
	void Update () {
        if (!Player1Controller.Instance.Dead)
        {
            if (Sight.PlayerInView)
            {
                CurrentState = State.CHASE;
            }
            if (CurrentState == State.WANDER)
            {
                Wander();
            }
            else if (CurrentState == State.CHASE)
            {
                Chase();
            }
        }
        else
        {
            Stop();
        }
	}

    void Wander()
    {
        Nav.speed = WanderSpeed;
        if (Nav.remainingDistance < Nav.stoppingDistance)
        {
            PatrolTimer += Time.deltaTime;
            if (PatrolTimer >= PatrolWaitTime)
            {
                if (PatrolPointIndex == PatrolPoints.Length - 1)
                {
                    PatrolPointIndex = 0;
                }
                else
                {
                    PatrolPointIndex++;
                }
                PatrolTimer = 0;
            }
        }
        else
        {
            PatrolTimer = 0;
        }
        Nav.destination = PatrolPoints[PatrolPointIndex].transform.position;

    }

    void Chase()
    {
        Nav.speed = ChaseSpeed;
        if(Sight.PlayerInView)
        {
            Nav.destination = Sight.LastSeen;
        }
        else if(!Sight.PlayerInView)
        {
            if(!Sight.PlayerInRadius)
            {
                if(Nav.remainingDistance < Nav.stoppingDistance)
                {
                    AlertTimer += Time.deltaTime;
                    if(AlertTimer >= AlertWaitTime)
                    {
                        AlertTimer = 0f;
                        CurrentState = State.WANDER;
                    }
                }
            }
            else
            {
                Nav.destination = Player.transform.position;
            }
        }
    }

    void Stop()
    {
        Nav.speed = 0;
        Nav.Stop();
    }
}
