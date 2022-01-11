using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[Serializable]
public class LogEvent
{
    public string title;
    public string details;
    public DateTime timestamp;
    public LogType level;
}

[Serializable]
public class LogBatch
{
    public LogEvent[] logEvents;
}

public class Logging : MonoBehaviour
{
    private LogEvent currentLog;
    private List<LogEvent> listLogEvents = new List<LogEvent>();
    [SerializeField] private LogBatch logBatch;
    //private Serilog.ILogger logToJsonFile;
    //private Serilog.ILogger logToWebService;

    //private void Awake()
    //{
    //    logToJsonFile = new LoggerConfiguration()
    //        .WriteTo
    //        .File(
    //            new JsonFormatter(renderMessage: true),
    //            Application.dataPath + "/AppLog.json"
    //        )
    //        .CreateLogger();
    //    Log.Logger = logToJsonFile;
    //}

    private void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
        Debug.Log(">>>>>>>>>>>> should WriteLogBatchToJson");
        WriteLogBatchToJson();
        // TODO: send batch batchErrors to rabbitMQ
        // if fail
        // log to file
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        currentLog = new LogEvent { title = logString, details = stackTrace, timestamp = DateTime.Now, level = type };
        // TODO: add different colors depending on logtype
        // TODO: do not log in prod env
        Debug.Log(currentLog);

        if (type == LogType.Error || type == LogType.Exception)
        {
            listLogEvents.Add(currentLog);
        }
    }

    private async void WriteLogBatchToJson()
    {
        logBatch = new LogBatch { logEvents = listLogEvents.ToArray() };
        string json = JsonUtility.ToJson(logBatch);
        using (StreamWriter writer = File.CreateText(Application.dataPath + "/ErrorLogs.json"))
        {
            await writer.WriteAsync(json);
        }
    }

}
