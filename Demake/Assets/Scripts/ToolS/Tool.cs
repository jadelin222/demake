using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ToolType
//{
//    None,
//    Shovel,
//    Hammer,
//    WateringCan,
//    Trumpet

//}
//abstract tool class
public abstract class Tool : MonoBehaviour
{
    public string toolName;
    public ItemType ToolType;

    public float maxIdleTime = 5f;
    public float coolDownTime = 1f;
    private float lastUseTime;
    private bool isHiding;

    public abstract void UseTool();
    public void ResetToolStatus()
    {
        isHiding = false;
        lastUseTime = Time.time; //idle time reset
        //gameObject.SetActive(true); 
        ShowTool();
        Debug.Log($"{toolName} equipped");
    }
   
    public void ActivateTool()
    {
        if (CanUse())
        {
            Debug.Log("Can use");
            UseTool();
            lastUseTime = Time.time; // restart counting cooldown and idle timer
        }
        else 
        {
            Debug.Log($"{toolName} is on cooldown");
        }
    }
    private void Update()
    {
        if (!isHiding && Time.time >= lastUseTime + maxIdleTime)
        {
            PutAway();
        }
    }
    public virtual void PutAway()
    {
        isHiding = true;
        PlayHideAnim();
        gameObject.SetActive(false);
        Debug.Log($"{toolName} is put away");
    }
    public bool CanUse()
    {
        //td: add condition to use on correct object?
        return Time.time >= lastUseTime + coolDownTime;
    }

    protected virtual void ShowTool()
    {
        Debug.Log("equipt anim played");
        //transform.localPosition = Vector3.zero; 
        gameObject.SetActive(true);
        isHiding = false;
    }
    protected virtual void PlayHideAnim()
    {
        //td: use animation
        //transform.localPosition += Vector3.down * 100;
        Debug.Log("hide anim played");

    }
}
