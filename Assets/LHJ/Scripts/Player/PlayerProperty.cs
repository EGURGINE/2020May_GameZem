using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Players
{
    public class PlayerProperty : MonoBehaviour
    {
        public static UnityEvent playerDead;

        [SerializeField] private float maxHP;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float maxBullet;
        [SerializeField] private BulletObejctPool bullets;
        [SerializeField] private Transform firePos;

        private float time;
        private float currentBullet;

        public static BulletObejctPool staticBullets;

        private List<Action<float>> collision = new List<Action<float>>();
        private float currentHP;

        private void Awake()
        {
            Player.playerProperty = this;
            staticBullets = bullets;
            currentBullet = maxBullet;

            collision.Add(SetHP);
            collision.Add(AddBullet);
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

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }

        void Fire()
        {

            if (currentBullet <= 0) return;
            if (time < fireRate) return;
            //น฿ป็
            time = 0;

            staticBullets.GetObj(firePos.position, transform.forward);
            currentBullet--;
            Debug.LogError(currentBullet);
        }

        public void AddBullet(float _amount)
        {
            currentBullet += _amount;
            if(currentBullet > maxBullet)
            {
                currentBullet = maxBullet;
            }
        }

        public void Reload()
        {
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
            }
        }

    }
}
