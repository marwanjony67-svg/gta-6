using UnityEngine;

public class MissionHUD : MonoBehaviour
{
    private static string message = "";

    public static void Show(string value)
    {
        message = value;
    }

    private void OnGUI()
    {
        if (string.IsNullOrEmpty(message)) return;

        GUI.Box(new Rect(20, 20, 420, 52), "");
        GUI.Label(new Rect(34, 34, 390, 28), message);
    }
}
