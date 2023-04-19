using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlPanel;
    public GameObject levelPanel;
    public GameObject rightHUD;
    public GameObject leftHUD;
    public GameObject HUD;
    public Text leftHUDButton;
    public string nextScene;

    public void Pause()
    {
        Time.timeScale = 0;
        HUD.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
    }

    public void ShowHideRightHUD()
    {
        rightHUD.SetActive(!rightHUD.activeSelf);
    }

    public void ShowHideLeftHUD()
    {
        leftHUD.SetActive(!leftHUD.activeSelf);
    }

    public void SwapArrowLeft()
    {
        if(leftHUDButton.text == ">")
        {
            leftHUDButton.text = "<";
        }
        else
        {
            leftHUDButton.text = ">";
        }
    }

    public void ShowControls()
    {
        pauseMenu.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void HideControls()
    {
        controlPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void LevelSelect()
    {
        pauseMenu.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void BackFromLevelSelect()
    {
        levelPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }

    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void LoadCutscene(int cutscene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(cutscene+5);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
