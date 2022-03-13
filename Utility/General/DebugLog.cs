using System;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    [SerializeField]
    private string debugMessage;

    public void SendDebugLog()
    {
        Debug.Log(debugMessage);
    }
    public void SendDebugLog(string message)
    {
        Debug.Log(message);
    }
}
