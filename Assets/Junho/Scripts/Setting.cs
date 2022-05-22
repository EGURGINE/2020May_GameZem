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
    public GameObject[] screenMode;
    private bool isFullScreen = true;// true : 풀스크린 , false : 창모드
    const int setWidth = 1920;
    const int setHeight = 1080;
    [SerializeField] private AudioSource bgm;
    public float volume;

    private void Awake()
    {
        float curSfx = PlayerPrefs.GetFloat("Volume");
        Debug.Log(curSfx);
        bgm.volume = curSfx;
        volume = curSfx;
    }
    public void WndChangeBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);

        if (isFullScreen)
        {
            screenMode[0].SetActive(false);
            screenMode[1].SetActive(true);
            isFullScreen = false;
        }
        else 
        {
            screenMode[0].SetActive(true);
            screenMode[1].SetActive(false);
            isFullScreen = true; 
        }

        Screen.SetResolution(setWidth, setHeight, isFullScreen);
    }

    public void SetMusicVolume(float setVolume)
    {
        bgm.volume = setVolume;
        volume = setVolume;
        PlayerPrefs.SetFloat("Volume", setVolume);
    }

    public void InExitBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        Time.timeScale = 1;
        GameManager.isSettingWnd = false;
        Cursor.lockState = CursorLockMode.Locked;
        settingWnd.SetActive(false);
    }
    public void TitleExitBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        Time.timeScale = 1;
        GameManager.isSettingWnd = false;
        Cursor.lockState = CursorLockMode.None;
        settingWnd.SetActive(false);
    }

    public void MainExitBtn()
    {
        SceneManager.LoadScene("Title");
    }
}
