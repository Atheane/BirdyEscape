using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Formatting.Json;


//TODO replace by serialog format message (time stamp etc)
// degager serialog
public class CustomError
{
    public string message;
    public string stackTrace;
    public DateTime timestamp;

}

public class Logging : MonoBehaviour
{
    private CustomError current;
    public List<CustomError> batchErrors = new List<CustomError>();
    private Serilog.ILogger logToJsonFile;
    //private Serilog.ILogger logToWebService;

    private void Awake()
    {
        logToJsonFile = new LoggerConfiguration()
            .WriteTo
            .File(
                new JsonFormatter(renderMessage: true),
                Application.dataPath + "/AppLog.json"
            )
            .CreateLogger();
        Log.Logger = logToJsonFile;
    }

    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
        // TODO: send batch batchErrors to rabbitMQ
        // if fail
        // log to file
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        current = new CustomError { message = logString, stackTrace = stackTrace, timestamp = DateTime.Now };
        // TODO: add different colors depending on logtype
        // TODO: do not log in prod env
        Debug.Log(current.message);

        if (type == LogType.Error || type == LogType.Exception)
        {
            batchErrors.Add(current);
        }

    }

}
