using System.Collections;
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
    public Animator HideNShowAnimator;
    public AnimationCurve showHideCurve;
    public AnimationCurve useToolCurve;

    public float maxIdleTime = 5f;
    public float coolDownTime = 1f;
    private float lastUseTime;
    private bool isHiding;

    private ToolState currentState = ToolState.Idle;

    public abstract void UseTool();

    //public void ActivateTool(bool hitValidTarget)
    public void ActivateTool()
    {
        //Debug.Log($"Attempting to activate tool: {toolName}");
        //cooldown check
        if (!CanUse())
        {
            //Debug.Log("Tool is on cooldown.");
            return;
        }
        if (currentState == ToolState.InUse)
        {
            //Debug.Log("Tool is already in use.");
            return;
        }
        //Debug.Log("Tool activated.");
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
        if(HideNShowAnimator != null)
            HideNShowAnimator.SetBool("IsInUse", state == ToolState.InUse);
    }
    public void ResetToolStatus()
    {
        isHiding = false;
        lastUseTime = Time.time; //idle time reset
        ShowTool();
        StartCoroutine(SetToIdleAfterDelay(0.2f));
    }
    private IEnumerator SetToIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetState(ToolState.Idle);
    }
    public virtual void PutAway()
    {
        //isHiding = true;
        //StartCoroutine(PlayHideAnimation());
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("cant start coroutine because thingy is inactive");
            return;
        }

        isHiding = true;
        StartCoroutine(PlayHideAnimation());
    }
    public bool CanUse() //if its on cd
    {
        //td: add condition to use on correct object?
        return Time.time >= lastUseTime + coolDownTime;
    }
    public void ResetStateToIdle() //event attached to end of tool use animation
    {
        SetState(ToolState.Idle);
    }
    public bool IsInUse()
    {
        return currentState == ToolState.InUse;
    }

    protected virtual void ShowTool()
    {
        if (isHiding) return;
        //play anim
        HideNShowAnimator.SetTrigger("ShowTool");
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
            StartCoroutine(AnimateToolUse());
        }
    }
    private IEnumerator AnimateToolUse()
    {
        float duration = 1f; //duration of the tool use animation
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = useToolCurve.Evaluate(t);
            animator.speed = curveValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        animator.speed = 1f; 
    }
    protected void PlayHideAnim()
    {
        HideNShowAnimator.SetTrigger("HideTool");
    }
    private IEnumerator PlayHideAnimation()
    {
        PlayHideAnim();
        float duration = 2f; //duration of the hide animation
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = showHideCurve.Evaluate(t);
            HideNShowAnimator.speed = curveValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        //yield return new WaitForSeconds(2f);
        //gameObject.SetActive(false);
    }
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
