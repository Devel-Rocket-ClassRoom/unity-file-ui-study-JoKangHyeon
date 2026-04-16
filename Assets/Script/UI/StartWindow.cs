using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button continueButton;
    public Button newGameButton;
    public Button optionButton;

    public bool canContinue;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue);
        newGameButton.onClick.AddListener(OnNewGame);
        optionButton.onClick.AddListener(OnOption);
    }

    public override void Open()
    {
        base.Open();
        continueButton.gameObject.SetActive(canContinue);
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnContinue()
    {
        Debug.Log("OnContinue");
    }

    public void OnNewGame()
    {
        Debug.Log("OnNewGame");
    }

    public void OnOption()
    {
        Debug.Log("OnOption");
    }
}
