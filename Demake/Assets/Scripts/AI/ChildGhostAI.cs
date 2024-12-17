using UnityEngine;
using UnityEngine.AI;

public class ChildGhostAI : MonoBehaviour, IInteractable
{
    public enum ChildState { Idle, Leading, WaitingForItem, WaitingForPlayer }
    public ItemType RequiredItem => ItemType.None;

    public string InteractionVerb => "Talk";

    [Header("Child Ghost Settings")]
    public ChildState currentState = ChildState.Idle;
    public Transform player;
    public float followDistance = 2f; // Keep close to player
    public float resumeDistance = 2.5f;        // Distance at which the ghost resumes moving
    public float lookAtSpeed = 2f;
    public Transform destinationPoint;
    public Animator animator;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true; // Idle state
    }
    private void Update()
    {
        if (currentState == ChildState.Leading)
        {
            HandleLeadingBehavior();
        }
        else if (currentState == ChildState.WaitingForPlayer)
        {
            HandleWaitingBehavior();
        }
    }
    public void Interact()
    {
        bool hasRequiredItem = ItemSystem.Instance.HasItem(ItemType.Candy);
        if (currentState == ChildState.Idle) 
        {
            UIManager.Instance.ShowBottomScreenUI("i have been waiting for mum forever");
            currentState = ChildState.WaitingForItem;
        }
        else if(currentState == ChildState.WaitingForItem && !hasRequiredItem)
        {
            UIManager.Instance.ShowBottomScreenUI("Bring me the thing please");
        }
        if (currentState == ChildState.WaitingForItem && hasRequiredItem)
        {
            UIManager.Instance.ShowBottomScreenUI("follow me");
            currentState = ChildState.Leading;
            LeadWay();
        }
    }
    public void OnRayHit()
    {
        UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }
    private void LeadWay()
    {
        agent.isStopped = false;
        //animation td
        agent.SetDestination(destinationPoint.position);
        PlayWalkAnimation();
    }
    private void HandleLeadingBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //stop if the player has fallen behind
        if (distanceToPlayer > followDistance)
            StopAndWait();
        else
            //keep moving to the destination
            agent.SetDestination(destinationPoint.position);
    }

    private void StopAndWait()
    {
        currentState = ChildState.WaitingForPlayer;
        agent.isStopped = true;
        PlayWaitingAnimation();
    }

    private void HandleWaitingBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        LookAtPlayer();

        if (distanceToPlayer <= resumeDistance)
            ResumeLeading();
    }

    private void ResumeLeading()
    {
        currentState = ChildState.Leading;
        agent.isStopped = false;
        PlayWalkAnimation();
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookAtSpeed);
    }
    private void PlayWaitingAnimation()
    {
        //standing and wait 
    }

    private void PlayWalkAnimation()
    {
        //keep walking
    }
}