using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainButtons;
    [SerializeField] private GameObject LevelSelect;
    [SerializeField] private GameObject Options;

    [SerializeField] private TMP_Text TimeText;
    [SerializeField] private TMP_Text CarText;

    private string[] carNames =
    {
        "Camero",
        "Haas",
    };

    private int carIndex = 0;

    public void SetLevelName(string level)
    {
        PlayerPrefs.SetString("level", level);
    }

    public void GotoLevelSelect()
    {
        MainButtons.SetActive(false);
        LevelSelect.SetActive(true);
        Options.SetActive(false);
    }
    
    public void GotoMainButton()
    {
        MainButtons.SetActive(true);
        LevelSelect.SetActive(false);
        Options.SetActive(false);
    }
    
    public void GotoOptions()
    {
        MainButtons.SetActive(false);
        LevelSelect.SetActive(false);
        Options.SetActive(true);
    }
    
    public void ToggleTimeSelect()
    {
        TimeText.text = TimeText.text.Equals("Night") ? "Day" : "Night";
        PlayerPrefs.SetString("time", TimeText.text);
    }
}