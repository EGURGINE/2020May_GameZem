using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScene : MonoBehaviour
{
    public void OnClickStartGameScene()
    {
        SceneManager.LoadScene(0);
    }
}
