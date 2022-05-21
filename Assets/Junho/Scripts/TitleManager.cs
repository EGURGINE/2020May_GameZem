using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject credit;
    [SerializeField] GameObject settingWnd;
    public void StartBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        SceneManager.LoadScene("GameScene");
    }
    public void SettingBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        settingWnd.SetActive(true);
    }
    public void CreditBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
    }
    public void ExitBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        Application.Quit();
    }
}
