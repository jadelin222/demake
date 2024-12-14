
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
