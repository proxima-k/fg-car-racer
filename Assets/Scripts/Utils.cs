#if UNITY_EDITOR
using UnityEngine;
#endif

public static class Utils {
    public static string FormatTime(float timeInSeconds) {
        int minute = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        int milliseconds = (int)(timeInSeconds * 100 % 100);
        return $"{minute:00}:{seconds:00}:{milliseconds:00}";
    }
    
#if UNITY_EDITOR
    public static void Log(string message) {
        Debug.Log(message);
    }

    public static void LogWarning(string message) {
        Debug.LogWarning(message);
    }

    public static void LogError(string message) {
        Debug.LogError(message);
    }
#endif
}
