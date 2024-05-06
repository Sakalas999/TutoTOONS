using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject levelSelectionWindow, mainMenuWindow, levelSelectionDropdown,  menuWindow, winScreen, winScreenText, winScreenButton1, winScreenButton2;

    int selectedLevelIndex = 0;

    public static UIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ShowWinScreen()
    {
        if (!winScreen.active)
            AudioManager.Instance.PlayLevelWonTheme();

        winScreen.SetActive(true);

        if(GameData.Instance.levelToLoadIndex == GameData.Instance.myLevelList.levels.Length-1)
        {
            winScreenButton1.SetActive(false);
        }
        else
        {
            winScreenButton2.SetActive(false);
            winScreenText.SetActive(false);
        }
    }

    public void NextLevel()
    {
        if (GameData.Instance.levelToLoadIndex < GameData.Instance.myLevelList.levels.Length - 1)
        {
            GameData.Instance.levelToLoadIndex++;
            SceneManager.LoadScene(1);
        }
    }

    public void PlayButton()
    {
        AudioManager.Instance.PlayUI();
        SceneManager.LoadScene(1);
    }

    public void LevelSelectionButton()
    {
        AudioManager.Instance.PlayUI();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuWindow.SetActive(false);
            levelSelectionWindow.SetActive(true);
            FindHowManyLevels();
            Debug.Log(GameData.Instance.myLevelList.levels.Length);
        }
        else
        {
            menuWindow.SetActive(false);
            levelSelectionWindow.SetActive(true);
            FindHowManyLevels();
        }
    }

    public void XButton()
    {
        AudioManager.Instance.PlayUI();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuWindow.SetActive(true);
            levelSelectionWindow.SetActive(false);
        }
        else
        {
            menuWindow.SetActive(true);
            levelSelectionWindow.SetActive(false);
        }
    }

    public void QuitButton()
    {
        AudioManager.Instance.PlayUI();
        Application.Quit();
    }

    public void SetSelectedLevelIndex(int val)
    {
        selectedLevelIndex = val;
        Debug.Log(val);
    }

    public void ContinueButton()
    {
        AudioManager.Instance.PlayUI();

        GameData.Instance.levelToLoadIndex = selectedLevelIndex;
        Debug.Log(selectedLevelIndex);
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        AudioManager.Instance.PlayUI();

        Destroy(GameData.Instance);
        SceneManager.LoadScene(0);
    }

    public void Gearbutton()
    {
        AudioManager.Instance.PlayUI();

        if (menuWindow.active)
        {
            menuWindow.SetActive(false);
        }
        else if(levelSelectionWindow.active)
        {
            levelSelectionWindow.SetActive(false);
        }
        else
        {
            if (!winScreen.active)
                menuWindow.SetActive(true);
        }
    }

    void FindHowManyLevels()
    {
        levelSelectionDropdown.GetComponent<TMP_Dropdown>().options.Clear();

        for (int i = 0; i < GameData.Instance.myLevelList.levels.Length; i++)
        {
            string newOption = "Level " + (i+1);
            levelSelectionDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(newOption, null));
        }
    }
}
