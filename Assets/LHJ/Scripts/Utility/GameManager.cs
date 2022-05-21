using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject puppy;

    [SerializeField] Transform playerSpawnPoint;
    [SerializeField] Transform puppySpawnPoint;

    [SerializeField] ItemPool itemPool;
    static ItemPool staticItemPool;

    [SerializeField] float itemSpawnDelay;
    [SerializeField] int maxItemCount;
    static int currentItemCount;
    float time = 0;

    [SerializeField] GameObject gameOver;
    [SerializeField] Text score;
    [SerializeField] Text surviveTimeTxt;
    float surviveTime;

    [SerializeField] private GameObject settingWnd;//¼³Á¤Ã¢
    private bool isSettingWnd;

    private Transform playerTrans;

    private void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        var p = Instantiate(player);
        var pu = Instantiate(puppy);

        p.transform.position = playerSpawnPoint.position;
        pu.transform.position = puppySpawnPoint.position;

        currentItemCount = maxItemCount;

        staticItemPool = itemPool;
    }


    private void Start()
    {
        Puppy.puppyDead += GameOver;
        Players.PlayerProperty.playerDead += GameOver;

        playerTrans = Players.Player.playerProperty.GetPlayerObj().transform;
    }

    private void OnDisable()
    {
        Puppy.puppyDead -= GameOver;
        Players.PlayerProperty.playerDead -= GameOver;
    }

    private void OnDestroy()
    {
        Puppy.puppyDead -= GameOver;
        Players.PlayerProperty.playerDead -= GameOver;
    }



    public void Update()
    {
        time += Time.deltaTime;
        surviveTime += Time.deltaTime;

        if(time > itemSpawnDelay)
        {
            time = 0;
            if (currentItemCount > maxItemCount) return;

            currentItemCount++;
            Vector3 randomPos = playerTrans.position;
            randomPos.x = UnityEngine.Random.Range(-30f, 30f) + playerTrans.position.x;
            randomPos.y = 0f;
            randomPos.z = UnityEngine.Random.Range(-30f, 30f) + playerTrans.position.z;

            staticItemPool.SetItemRandom(randomPos);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingWnd)
            {
                Time.timeScale = 1;
                isSettingWnd = false;
                Cursor.lockState = CursorLockMode.Locked;
                settingWnd.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                isSettingWnd = true;
                settingWnd.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    public static void ReturnItem(Item _item)
    {
        staticItemPool.Return(_item);
        if (currentItemCount > 1) currentItemCount--;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
        gameOver.SetActive(true);
        TimeTo60(surviveTime);
    }

    public void TimeTo60(float _time)
    {
        int time = Mathf.RoundToInt(_time);
        int second = time % 60;
        int min = time / 60;
        int hour = min / 60;
        min = min % 60;

        string s = second.ToString();
        string m = min.ToString();
        string h = hour.ToString();

        if (second < 10) s = "0" + second;
        if (min < 10) m = "0" + min;
        if (hour < 10) h = "0" + hour;

        surviveTimeTxt.text = "Time  :  " + h + ":" + m + ":" + s;

    }
}
