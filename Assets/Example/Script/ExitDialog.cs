using UnityEngine;

public class ExitDialog : Dialog<ExitDialog>
{
    public static void Show()
    {
        Open();
    }

    public static void Hide()
    {
        Close();
    }

    public void Quit()
    {
        Close();
        Application.Quit();
    }

    public void Rate()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
        Close();
    }
}