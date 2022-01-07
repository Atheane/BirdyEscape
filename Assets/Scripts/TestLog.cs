using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serilog;

public class TestLog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Log.Information("Test - Start(), Here is information log");
    }

}
