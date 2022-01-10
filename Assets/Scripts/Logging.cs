using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Formatting.Json;


public class Logging : MonoBehaviour
{
    void Awake()
    {
        var log = new LoggerConfiguration()
            .WriteTo
            .File(new JsonFormatter(), Application.dataPath + "/AppLog.json")
            .CreateLogger();

        Log.Logger = log;
    }

}
