using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneBrain : MonoBehaviour
{
    public List<Sprite> slides = new List<Sprite>();
    public SpriteRenderer slide;
    public int slideNum;
    public GameObject nextButton;
    public GameObject battleButton;
    bool battleOff = true;

    public string nextScene;

    void Update()
    {
        if (battleOff)
        {
            if (slideNum == slides.Count - 1)
            {
                slide.sprite = slides[slideNum];
                nextButton.SetActive(false);
                battleButton.SetActive(true);
                battleOff = false;
            }
            else
            {
                slide.sprite = slides[slideNum];
            }
        }
    }

    public void Increment()
    {
        slideNum++;
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }
}
