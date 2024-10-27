using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Tool
{
    public override void UseTool()
    {
        Debug.Log("watering plants...gluc gluc");
        WaterPlants();
    }
    private void WaterPlants()
    {
        Debug.Log("plants revived!");
    }
}

//public class WateringCan : MonoBehaviour, ITool
//{
//    public void UseTool()
//    {
//        WaterPlants();
//    }
//   
//    //private void WaterPlants(GameObject target)
//    //{
//    //    if (target.CompareTag("Plant")) // and other condition
//    //    {
//    //        Debug.Log("watering plant!!!");
//    //        //do something, anim
//    //        //revive plant  change a model    
//    //    }
//    //    else
//    //    {
//    //        Debug.Log("watering can has no use here");
//    //    }
//    //    Debug.Log("Plants revived!");
//    //}
//}