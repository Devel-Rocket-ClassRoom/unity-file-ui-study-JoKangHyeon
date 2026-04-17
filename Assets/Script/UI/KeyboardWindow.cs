using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI inputViewer;
    public Button cancelButton;
    public Button deleteButton;
    public Button acceptButton;

    public int maxCharacter = 7;
    public char cursor = '_';
    public float cursorBlinkInterval = 3;

    StringBuilder inputTextStringBuilder;
    string inputTextWithCursor;
    string inputTextWithoutCursor; 

    bool showCursor = false;
    float timer = 0f;

    private void Awake()
    {
        cancelButton.onClick.AddListener(OnCancel);
        deleteButton.onClick.AddListener(OnDelete);
        acceptButton.onClick.AddListener(OnAccept);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > cursorBlinkInterval)
        {
            timer = 0f;

            if (showCursor)
            {
                inputViewer.text = inputTextWithoutCursor;
            }
            else
            {
                inputViewer.text = inputTextWithCursor;
            }

            showCursor = !showCursor;
        }
    }

    public override void Open()
    {
        inputTextStringBuilder = new StringBuilder();
        showCursor = true;
        timer = 0f;
        UpdateText();

        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void Input(string c)
    {
        if (inputTextStringBuilder.Length < maxCharacter)
        {
            inputTextStringBuilder.Append(c);
            UpdateText();
        }
    }

    public void Delete()
    {
        if (inputTextStringBuilder.Length > 0)
        {
            inputTextStringBuilder.Remove(inputTextStringBuilder.Length - 1, 1);
            UpdateText();
        }
    }

    public void DeleteAll()
    {
        if (inputTextStringBuilder.Length > 0)
        {
            inputTextStringBuilder.Clear();
            UpdateText();
        }
    }

    public void UpdateText()
    {
        inputTextWithoutCursor = inputTextStringBuilder.ToString();

        if(inputTextStringBuilder.Length < maxCharacter)
        {
            inputTextStringBuilder.Append(cursor);
            inputTextWithCursor = inputTextStringBuilder.ToString();
            inputTextStringBuilder.Remove(inputTextStringBuilder.Length - 1, 1);
        }
        else
        {
            inputTextWithCursor = inputTextStringBuilder.ToString();
        }



        if (showCursor)
        {
            inputViewer.text = inputTextWithoutCursor;
        }
        else
        {
            inputViewer.text = inputTextWithCursor;
        }
    }

    public void OnCancel()
    {
        DeleteAll();
    }

    public void OnDelete()
    {
        Delete();
    }

    public void OnAccept()
    {
        windowManager.Open(0);
    }
}
