using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{

    public Toggle[] toggles;
    public int selected=2;

    public Button cancelButton;
    public Button applyButton;

    const string c_DifficultyPrefKey = "difficulty";

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        cancelButton.onClick.AddListener(OnCancel);
        applyButton.onClick.AddListener(OnApply);
    }

    public override void Open()
    {
        base.Open();

        selected = PlayerPrefs.GetInt(c_DifficultyPrefKey, 0);
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if (active)
        {
            Debug.Log("OnEasy");
            selected = 0;
        }
    }

    public void OnNormal(bool active)
    {
        if (active)
        {
            Debug.Log("OnNormal");
            selected = 1;
        }
    }

    public void OnHard(bool active)
    {
        if (active)
        {
            Debug.Log("OnHard");
            selected = 2;
        }
    }

    public void OnCancel()
    {
        windowManager.Open(0);
    }

    public void OnApply()
    {
        PlayerPrefs.SetInt(c_DifficultyPrefKey, selected);
        windowManager.Open(0);
    }
}
