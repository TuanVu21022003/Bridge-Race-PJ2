public class MainMenu : SimpleMenu<MainMenu>
{
    private void Start()
    {
    }

    public override void OnMenuBecameVisible()
    {
        base.OnMenuBecameVisible();
    }

    public override void OnMenuBecameInvisible()
    {
        base.OnMenuBecameInvisible();
    }

    public override void OnBackPressed()
    {
        ExitDialog.Show();
    }

    public void ShowSetting()
    {
        SettingMenu.Show();
    }

    public void ShowExampleMenu()
    {
        ExampleMenu.Show();
    }

    public void ShowExampleDialog()
    {
        ExampleDialog.Show();
    }

    public void Exit()
    {
        ExitDialog.Show();
    }
}