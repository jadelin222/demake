using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GetMainLightDir : MonoBehaviour
{
    [SerializeField]
    private Material skyBoxMat;
    [SerializeField]
    private GameTime time;

    public Light directionalLight;

    [Header("angles")]
    public float sunRiseX = -90f;
    public float sunsetX = 270f;
    void Update()
    {
        UpdateDirectionalLight();
        //skyBoxMat.SetVector("_mainLightDir", transform.forward);
        skyBoxMat.SetVector("_mainLightDir", directionalLight.transform.forward);    
    }

    private void UpdateDirectionalLight()
    {
        float currentAngle = Mathf.Lerp(sunRiseX, sunsetX, time.inGameTime);
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(currentAngle, -30f, 0f));
    }
}
