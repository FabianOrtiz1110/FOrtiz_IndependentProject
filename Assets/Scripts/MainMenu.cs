using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button rulesButton;  
    public GameObject title;
    public TextMeshProUGUI rules;
    public Button back;
    public Button quitButton;

    public void BeginGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowRules()
    {
        rules.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        rulesButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        rules.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        rulesButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}