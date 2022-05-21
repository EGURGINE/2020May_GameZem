using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject credit;
    [SerializeField] GameObject settingWnd;
    [SerializeField] GameObject rankingWnd;

    [SerializeField] Text[] rankTxt;
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

    public void RankingBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        rankingWnd.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            rankTxt[i].text = PlayerPrefs.GetFloat(i + "BestScore").ToString();
        }
    }
    public void RankExitBtn()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.BUTTON);
        rankingWnd.SetActive(false);
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
