using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieController : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    NavMeshAgent agent;
    public float walkingSpeed;
    public float runningSpeed;
    public enum STATE { IDLE, WONDER, CHASE, ATTACK, DEAD };
    public STATE state = STATE.IDLE;//default state
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (target == null && GameStart.isGameOver == false)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        switch (state)
        {
            case STATE.IDLE:
                if (CanSeePlayer())
                    state = STATE.CHASE;
                else if (Random.Range(0, 1000) < 5)
                {
                    state = STATE.WONDER;
                }
                break;
            case STATE.WONDER:
                if (!agent.hasPath)
                {
                    float randValueX = transform.position.x + Random.Range(-5f, 5f);
                    float randValueZ = transform.position.z + Random.Range(-5f, 5f);
                    float ValueY = Terrain.activeTerrain.SampleHeight(new Vector3(randValueX, 0f, randValueZ));
                    Vector3 destination = new Vector3(randValueX, ValueY, randValueZ);
                    agent.SetDestination(destination);
                    agent.stoppingDistance = 0f;
                    agent.speed = walkingSpeed;
                    TurnOffAllTriggerAnim();
                    anim.SetBool("isWalking", true);
                }
                if (CanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                else if (Random.Range(0, 1000) < 7)
                {
                    state = STATE.IDLE;
                    TurnOffAllTriggerAnim();
                    agent.ResetPath();
                }
                break;

            case STATE.CHASE:
                if (GameStart.isGameOver )
                {
                    TurnOffAllTriggerAnim();
                    state = STATE.WONDER;
                    return;
                }
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 2f;
                TurnOffAllTriggerAnim();
                anim.SetBool("isRunning", true);
                agent.speed = runningSpeed;
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = STATE.ATTACK;
                }
                if (CannotSeePlayer())
                {
                    state = STATE.WONDER;
                    agent.ResetPath();
                }
                break;

            case STATE.ATTACK:
                if (GameStart.isGameOver)
                {
                    TurnOffAllTriggerAnim();
                    state = STATE.WONDER;
                    return;
                }
                TurnOffAllTriggerAnim();
                anim.SetBool("isAttacking", true);
                transform.LookAt(target.transform.position);//Zombies should look at Player
                if (DistanceToPlayer() > agent.stoppingDistance + 2)
                {
                    state = STATE.CHASE;
                }
                print("Attack State");
                break;

            case STATE.DEAD:
                
                //GameObject tempRd = Instantiate(ragdollPrefab, this.transform.position, this.transform.rotation);
                //tempRd.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10000);
                Destroy(agent);
                break;
        }
    }
    public void TurnOffAllTriggerAnim()//All animation are off
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDead", false);
    }

    public bool CanSeePlayer()
    {
        if (DistanceToPlayer() < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float DistanceToPlayer()
    {
        if (GameStart.isGameOver)
        {
            return Mathf.Infinity;

        }
            return Vector3.Distance(target.transform.position, this.transform.position);
        
    }

    public bool CannotSeePlayer()
    {
        if (DistanceToPlayer() > 20f)
        {
            return true;
        }
        else
            return false;
    }

    public void KillZombie()
    {
        TurnOffAllTriggerAnim();
        anim.SetBool("isDead",true);
        state = STATE.DEAD;
    }

    int damageAmount = 5;
    public void DamagePlayer()
    {
        if (target!=null)
        {
            //target.GetComponent<PlayerController>().TakeHit(damageAmount);//create a method Random sound when player takes damage
        }

       

    }
}

public class GameStart
{
    public static bool isGameOver = false;
}