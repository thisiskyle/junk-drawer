using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;



public static class Json
{
    public static T Read<T>(string path)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest reader = new UnityWebRequest(path);
            while (!reader.isDone) { }
            return JsonUtility.FromJson<T>(reader.downloadHandler.text);
        }
        else
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }
    }

    public static void Write<T>(string path, T jsonObj)
    {
        File.WriteAllText(path, JsonUtility.ToJson(jsonObj));
    }
}
