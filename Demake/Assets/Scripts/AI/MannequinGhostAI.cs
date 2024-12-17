using UnityEngine;
using UnityEngine.AI;

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
    private bool isActivated = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }
    private void Update()
    {
        if (!isActivated) return; //do nothing until activated thru activator triggerr!

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
}