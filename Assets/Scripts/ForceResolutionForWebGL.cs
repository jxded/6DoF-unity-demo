using UnityEngine;

public class ForceResolutionForWebGL : MonoBehaviour
{
    void Awake()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        Screen.SetResolution(1920, 1080, false);
        #endif
    }
    
    }
