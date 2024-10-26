using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour, ITool
{
    //private void Awake()
    //{
    //    toolName = "Watering Can";
    //    description = "water the plants";
    //}
    public void UseTool()
    {
        WaterPlants();

    }

    private void WaterPlants()
    {
        //if (target.CompareTag("Plant")) // and other condition
        //{
        //    Debug.Log("watering plant!!!");
        //    //do something, anim
        //    //revive plant  change a model    
        //}
        //else
        //{
        //    Debug.Log("watering can has no use here");
        //}
        Debug.Log("Plants revived!");
    }

    //private void WaterPlants(GameObject target)
    //{
    //    if (target.CompareTag("Plant")) // and other condition
    //    {
    //        Debug.Log("watering plant!!!");
    //        //do something, anim
    //        //revive plant  change a model    
    //    }
    //    else
    //    {
    //        Debug.Log("watering can has no use here");
    //    }
    //    Debug.Log("Plants revived!");
    //}


    //public override void UseTool(GameObject target)
    //{
    //    if (target.CompareTag("Plant"))
    //    {
    //        Debug.Log("Using watering can to water plant.");
    //        //revive plant  change a model
    //    }
    //    else
    //    {
    //        Debug.Log("Watering can can only be used on plants.");
    //    }
    //}

   
}