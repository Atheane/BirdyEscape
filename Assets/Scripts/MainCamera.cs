using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        throw new Exception("Main Camera Starts 3333");
    }
}
