using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Players
{
    public class PlayerProperty : MonoBehaviour, ICollision
    {
        public static Action playerDead;

        [SerializeField] private float maxHP = 100;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float maxBullet;
        [SerializeField] private float currentMagazine;

        [SerializeField] private BulletObejctPool bullets;
        [SerializeField] private Transform firePos;

        [SerializeField] Text currentMagazineTxt;
        [SerializeField] Text currentBulletTxt;
        [SerializeField] Text maxBulletTxt;
        [SerializeField] GameObject notifyReload;
        [SerializeField] GameObject isRoadingUI;
        [SerializeField] Image hpBar;

        private float time;
        private float currentBullet;

        public static BulletObejctPool staticBullets;

        private List<Action<float>> collision = new List<Action<float>>();
        private float currentHP;
        private bool isReloading;

        private void Awake()
        {
            Player.playerProperty = this;
            staticBullets = bullets;
            currentBullet = maxBullet;
            currentHP = maxHP;

            collision.Add(SetHP);
            collision.Add(AddMagazine);
            collision.Add(AddScore);

            maxBulletTxt.text = maxBullet.ToString();
            currentMagazineTxt.text = "ÅºÃ¢ ¼ö : " + currentMagazine;
            currentBulletTxt.text = currentBullet.ToString();
        }

        public void SetHP(float _amount)
        {
            currentHP -= _amount;

            if(currentHP > maxHP)
            {
                currentHP = maxHP;
            }

            hpBar.fillAmount = currentHP / maxHP;

            if (currentHP <= 0)
            {
                playerDead?.Invoke();
            }
        }

        private void Update()
        {
            time += Time.fixedDeltaTime;

            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            {
                Fire();
            }

            if(Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                Reload();
            }
        }

        void Fire()
        {

            if (currentBullet <= 0) return;
            if (time < fireRate) return;
            //¹ß»ç
            time = 0;
            SoundManager.Instance.PlaySound(Sound_Effect.SHOT);
            staticBullets.GetObj(firePos.position, transform.forward);
            currentBullet--;
            currentBulletTxt.text = currentBullet.ToString();
            if (currentBullet <= 0)
                notifyReload.SetActive(true);
        }

        public void AddBullet(float _amount)
        {
            currentBullet += _amount;
            if(currentBullet > maxBullet)
            {
                currentBullet = maxBullet;
            }
            currentBulletTxt.text = currentBullet.ToString();
        }

        public void AddMagazine(float _amount)
        {
            currentMagazine += _amount;
            currentMagazineTxt.text = "ÅºÃ¢ ¼ö : " + currentMagazine;
        }

        public void AddScore(float _amount)
        {
            GameManager.score += _amount;
        }

        public void Reload()
        {
            if (currentMagazine <= 0) return;

            notifyReload.SetActive(false);
            StartCoroutine(Reloading());
        }

        IEnumerator Reloading()
        {
            if (isReloading) yield break;
            isRoadingUI.SetActive(true);
            isReloading = true;

            AddMagazine(-1);
            

            yield return new WaitForSeconds(1f);
            isReloading = false;
            isRoadingUI.SetActive(false);
            AddBullet(maxBullet);
        }

        public GameObject GetPlayerObj()
        {
            return this.gameObject;
        }

        public void OnTriggerEnter(Collider other)
        {
            Item item = other.GetComponent<Item>();
            if(item != null)
            {
                int num = (int)item.itemType;
                collision[num].Invoke(item.amount);
                GameManager.ReturnItem(item);
            }
        }

        public void Collide()
        {
            SetHP(10f);
        }

        bool damaging = false;

        public void OnCollisionStay(Collision collision)
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            //ICollision icollision = this.GetComponent<ICollision>();
            if (enemy != null)
            {
                StartCoroutine(GetDamage(1f));
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            damaging = false;
        }

        public IEnumerator GetDamage(float _time)
        {
            if (damaging) yield break;

            damaging = true;

            while (true)
            {
                SetHP(10f);
                yield return new WaitForSeconds(_time);
                Debug.Log("damaging");
                if (!damaging) yield break;
            }
        }
    }
}
