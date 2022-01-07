using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serilog;
using Serilog.Sinks.File;

public class Logging : MonoBehaviour
{
    void Awake()
    {
        var log = new LoggerConfiguration().WriteTo.File(Application.dataPath + "/AppLog.txt").CreateLogger();
        Log.Logger = log;
    }

}
