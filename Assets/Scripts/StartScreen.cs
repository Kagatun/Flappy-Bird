using System;

public class StartScreen : Window
{
    public event Action PlayButtonClicked;

    public override void Close()
    {
        //WindowGroup.alpha = 0f;
        //ActionButton.interactable = false;
        gameObject.SetActive(false);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    protected override void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}
