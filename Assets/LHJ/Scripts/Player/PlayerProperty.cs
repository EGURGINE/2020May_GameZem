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
        [SerializeField] GameObject isRoadingUI;

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

            if(currentHP <= 0)
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

            staticBullets.GetObj(firePos.position, transform.forward);
            currentBullet--;
            currentBulletTxt.text = currentBullet.ToString();
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

        public void Reload()
        {
            if (currentMagazine <= 0) return;

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
    }
}
