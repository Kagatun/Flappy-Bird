using System;

public class RestartScreen : Window
{
    public event Action RestartButtonClicked;

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
