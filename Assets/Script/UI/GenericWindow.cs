using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour
{
    public GameObject firstSelected;

    protected WindowManager windowManager;

    public void Init(WindowManager windowManager)
    {
        this.windowManager = windowManager;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
