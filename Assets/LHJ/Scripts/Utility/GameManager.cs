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
    float[] bestScore = new float[5];
    [SerializeField] Text[] bestScoreTxt;

    [SerializeField] GameObject gameOver;
    [SerializeField] Text scoreTxt;
    [SerializeField] Text surviveTimeTxt;
    float surviveTime;

    [SerializeField] private GameObject settingWnd;//����â
    private bool isSettingWnd;

    [SerializeField] private Text playTime;

    //score
    public static float score;

    [SerializeField] private Transform plane;
    private Transform playerTrans;

    [SerializeField] private GameObject[] mapObjs;
    private void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        var p = Instantiate(player);
        //var pu = Instantiate(puppy);

        p.transform.position = playerSpawnPoint.position;
        //pu.transform.position = puppySpawnPoint.position;

        currentItemCount = maxItemCount;

        staticItemPool = itemPool;

        score = 0;
        for (int i = 0; i < 1000; i++)
        {
            GameObject obj = Instantiate(mapObjs[Random.Range(0, mapObjs.Length)]);
            obj.transform.position = 
                new Vector3(Random.Range(-200f, 200f), 0, Random.Range(-200f, 200f));
            obj.transform.SetParent(plane);
        }
        
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

        playTime.text = TimeToString(surviveTime);

        if (time > itemSpawnDelay)
        {
            time = 0;
            if (currentItemCount > maxItemCount) return;

            currentItemCount++;
            Vector3 randomPos = playerTrans.position;
            randomPos.x = UnityEngine.Random.Range(-15f, 15f) + playerTrans.position.x;
            randomPos.y = 0.5f;
            randomPos.z = UnityEngine.Random.Range(-15f, 15f) + playerTrans.position.z;

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
        SetScore(score);
        //scoreTxt.text = score.ToString();
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
    void SetScore(float curScore)
    {
        float temp = 0;
        for (int i = 0; i < 5; i++)
        {
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");

            while (bestScore[i] < curScore)
            {
                temp = bestScore[i];
                bestScore[i] = curScore;

                PlayerPrefs.SetFloat(i + "BestScore", curScore);
                curScore = temp;
            }

        }

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestTime", bestScore[i]);
        }

        int rank = 1;
        for (int i = 0; i < 5; i++)
        {

            bestScoreTxt[i].text = "Ranking " + rank + " : " + bestScore[i];
            rank++;
        }
        scoreTxt.text = "Score : " + curScore;
    }

    public string TimeToString(float _time)
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

        return h + ":" + m + ":" + s;
    }
}
