using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;


[Serializable]
public class LogEvent
{
    public string title;
    public string details;
    public LogType level;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}

public class Logging : MonoBehaviour
{
    private LogEvent currentLog;
    private List<LogEvent> listLogEvents;
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
    private async void Awake()
    {
        Debug.Log(">>>>>>>>>>>> should ReadLogBatchFromJsonAsync");
        //TODO parse errorLogs json file. Populate LogEvent.
        string json = await ReadLogBatchFromJsonAsync();
        //Debug.Log(">>>>>>> json");
        //Debug.Log(json);
        if (json != null)
        {
            Debug.Log(">>>>>>> json !=null");
            Debug.Log(">>>>>>> json");
            Debug.Log(json);

            LogEvent[] logEvents = JsonHelper.FromJson<LogEvent>(json);
            Debug.Log(">>>>>>> logEvents");
            Debug.Log(logEvents);
            string json2 = JsonHelper.ToJson(logEvents);
            Debug.Log(">>>>>>> json2");
            Debug.Log(json2);

            listLogEvents = new List<LogEvent>(logEvents);
            Debug.Log(">>>>>>> listLogEvents");
            Debug.Log(listLogEvents);
        } else
        {
            Debug.Log("<<<<<<<<<< json is null");
            listLogEvents = new List<LogEvent>();
        }
    }

    private void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
        Debug.Log(">>>>>>>>>>>> should WriteLogBatchToJson");
        WriteLogBatchToJsonAsync();
        // TODO: send batch batchErrors to rabbitMQ
        // if fail
        // log to file
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        currentLog = new LogEvent { title = logString, details = stackTrace, level = type };
        // TODO fix timestamp
        // TODO: add different colors depending on logtype
        // TODO: do not log in prod env
        Debug.Log(currentLog);

        if (type == LogType.Error || type == LogType.Exception)
        {
            listLogEvents.Add(currentLog);
        }
    }

    private async void WriteLogBatchToJsonAsync()
    {
        string json = JsonHelper.ToJson(listLogEvents.ToArray());
        using (StreamWriter writer = File.CreateText(Application.dataPath + "/ErrorLogs.json"))
        {
            await writer.WriteAsync(json);
        }
    }


    private async Task<string> ReadLogBatchFromJsonAsync()
    {
        using (var sr = new StreamReader(Application.dataPath + "/ErrorLogs.json"))
        {
            return await sr.ReadToEndAsync();
        }
    }

}
