using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject interactButton;

    public static UIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        interactButton.SetActive(false);
    }

    public void ShowButton()
    {
        interactButton.SetActive(true);
    }

    public void HideButton()
    {
        interactButton.SetActive(false);
    }
}