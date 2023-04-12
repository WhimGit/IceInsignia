using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject rightHUD;
    public GameObject leftHUD;
    public Text leftHUDButton;
    public Text rightHUDButton;
    public string nextScene;

    void Start()
    {
        //SceneManager.LoadScene(nextScene);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
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

    public void SwapArrowRight()
    {
        if (rightHUDButton.text == ">")
        {
            rightHUDButton.text = "<";
        }
        else
        {
            rightHUDButton.text = ">";
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
