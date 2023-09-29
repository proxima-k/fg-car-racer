#if UNITY_EDITOR
using UnityEngine;

public static class Logger  {
    public static void Log(string message) {
        Debug.Log(message);
    }

    public static void LogWarning(string message) {
        Debug.LogWarning(message);
    }

    public static void LogError(string message) {
        Debug.LogError(message);
    }
}

#endif
