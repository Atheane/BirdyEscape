using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serilog;

public class MainCamera : MonoBehaviour
{
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Log.Information("Dividing {A} by {B}", 1.2f, 2.0f);
        Debug.Log("Dividing {A} by {B}");

    }
}
