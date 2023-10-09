
public static class Utils {
    public static string FormatTime(float timeInSeconds) {
        int minute = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        int milliseconds = (int)(timeInSeconds * 100 % 100);
        return $"{minute:00}:{seconds:00}:{milliseconds:00}";
    }
}
