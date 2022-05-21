using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Setting : MonoBehaviour
{
    private static Setting instance;
    public static Setting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Setting>();
            }
            return instance;
        }
    }

    [SerializeField] GameObject settingWnd;
    [SerializeField] private Text screenMode;
    private bool isFullScreen = true;// true : 풀스크린 , false : 창모드
    const int setWidth = 1920;
    const int setHeight = 1080;

    [SerializeField] private AudioSource bgm;
    public float volume;
    public void WndChangeBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);

        if (isFullScreen)
        {
            screenMode.text = "창 모드";
            isFullScreen = false;
        }
        else 
        {
            screenMode.text = "전체화면";
            isFullScreen = true; 
        }

        Screen.SetResolution(setWidth, setHeight, isFullScreen);
    }

    public void SetMusicVolume(float setVolume)
    {
        bgm.volume = setVolume;
        volume = setVolume;
    }

    public void ExitBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        settingWnd.SetActive(false);
    }

    public void MainExitBtn()
    {
        SceneManager.LoadScene("Title");
    }
}
