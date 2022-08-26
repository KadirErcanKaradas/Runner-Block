using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.iOS;

public class CameraFOV : MonoBehaviour
{
    [SerializeField] private float PlusFOV = 60.0f;
    [SerializeField] private float XSeriesFOV = 72.0f;
    
    void Awake()
    {
        CameraField();
    }

   
    private void CameraField() 
    {
        DeviceGeneration device = Device.generation;
        if (device == DeviceGeneration.iPhoneX || device == DeviceGeneration.iPhoneXR || device == DeviceGeneration.iPhoneXSMax || device == DeviceGeneration.iPhoneXS || device == DeviceGeneration.iPhone11 || device == DeviceGeneration.iPhone11Pro || device == DeviceGeneration.iPhone11ProMax)
        {
            Camera.main.fieldOfView = XSeriesFOV;  

        }
        else
        {
            Camera.main.fieldOfView = PlusFOV;
        }
    }
}
