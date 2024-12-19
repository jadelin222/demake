using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static GameMaster;

public class MannequinGhostAI : MonoBehaviour
{
    public enum GhostState { Idle, Chasing }

    [Header("Ghost Settings")]
    public GhostState currentState = GhostState.Idle;
    public Transform player;
    public float chaseDistance = 20f;      
    public float stopChaseDistance = 30f;
    public Animator animator;

    private NavMeshAgent agent;
    private bool isIdleLocked = false;
    private bool isActivated = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }
    private void Update()
    {
        //do nothing until activated thru activator triggerr! or when ghost locked to idle due to trumpet effect
        if (isIdleLocked || !isActivated) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case GhostState.Idle:
                if (distanceToPlayer <= chaseDistance) //start chasing when player is close
                    StartChase();
                break;

            case GhostState.Chasing:
                if (distanceToPlayer > stopChaseDistance) //stop chasing if player is far
                    StopChase();
                else
                    StartChase();
                break;
        }
    }
    public void ActivateGhost()
    {
        if (!isActivated)
            isActivated = true;
        else return;
    }
    private void StartChase()
    {
        agent.SetDestination(player.position);
        currentState = GhostState.Chasing;
        agent.isStopped = false;
    }
    private void StopChase()
    {
        agent.isStopped = true;
        currentState = GhostState.Idle;
    }
    //when trumpet sings. 
    public void SetGhostStateIdle(float duration = 0f)
    {
        isIdleLocked = true; //lock the state to idle for the duration
        currentState = GhostState.Idle;
        agent.isStopped = true;

        StartCoroutine(ResumeAfterDelay(duration));
    }

    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isIdleLocked = false; 
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Endings endingType = Endings.CaughtByGhosts;
            //GameMaster.Instance.TriggerEnding(endingType);
            StartCoroutine(HandlePlayerCaught());
        }
    }
    private IEnumerator HandlePlayerCaught()
    {
        //move ghost in front of player
        Vector3 playerPosition = player.position;
        transform.position = playerPosition + player.forward * 2f;
        transform.LookAt(player);

        //animator.SetTrigger("Scare");

        yield return new WaitForSeconds(2f);

        FadeManager.Instance.FadeIn(() => GameMaster.Instance.TriggerEnding(Endings.CaughtByGhosts));
    }
}