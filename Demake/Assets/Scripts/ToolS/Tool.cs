using UnityEngine;

public enum ToolState
{
    Idle,
    InUse
}

public abstract class Tool : MonoBehaviour
{
    public string toolName;
    public ItemType ToolType;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    //public AudioClip hitEmptyClip;
    public AudioClip hitClip;

    [Header("Animation Settings")]
    public Animator animator;


    public float maxIdleTime = 5f;
    public float coolDownTime = 1f;
    private float lastUseTime;
    private bool isHiding;

    private ToolState currentState = ToolState.Idle;

    public abstract void UseTool();

    //public void ActivateTool(bool hitValidTarget)
    public void ActivateTool()
    {
        //cooldown check
        if (!CanUse())
            return;
        if (currentState == ToolState.InUse)
            return;

        PlayHitAnim();
        PlayHitSound();
        UseTool();
        lastUseTime = Time.time;

    }
    private void Update()
    {
        //hide tool after max idle time
        if (!isHiding && Time.time >= lastUseTime + maxIdleTime)
        {
            PutAway();
        }
        //if the tool is in use but cooldown has passed, reset to Idle
        if (currentState == ToolState.InUse && Time.time >= lastUseTime + coolDownTime)
        {
            ResetStateToIdle();
        }
    }
    private void SetState(ToolState state)
    {
        currentState = state;
        if (animator != null)
            animator.SetBool("IsIdle", state == ToolState.Idle);
    }
    public void ResetToolStatus()
    {
        isHiding = false;
        lastUseTime = Time.time; //idle time reset
        ShowTool();
    }
   
    public virtual void PutAway()
    {
        isHiding = true;
        //PlayHideAnim();
        gameObject.SetActive(false);
    }
    public bool CanUse() //if its on cd
    {
        //td: add condition to use on correct object?
        return Time.time >= lastUseTime + coolDownTime;
    }
    private void ResetStateToIdle() //event attached to end of tool use animation
    {
        SetState(ToolState.Idle);
    }

    protected virtual void ShowTool()
    {
        Debug.Log("equipt anim played");
//play anim
        gameObject.SetActive(true);
        isHiding = false;
        SetState(ToolState.Idle);
    }
    protected void PlayHitAnim()
    {
        if (animator != null)
        {
            animator.SetBool("IsIdle", false);
            SetState(ToolState.InUse);
        }
    }
    //protected void PlayHideAnim()
    //{
    //    if (animator != null)
    //        animator.SetTrigger("PutAway");
    //}

    protected void PlayHitSound()
    {
        if (audioSource != null && hitClip != null)
            audioSource.PlayOneShot(hitClip);
    }

    //protected void PlayHitEmptySound()
    //{
    //    if (audioSource != null && hitEmptyClip != null)
    //        audioSource.PlayOneShot(hitEmptyClip);
    //}

 
}
