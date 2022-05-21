using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Players
{
    public class PlayerProperty : MonoBehaviour
    {
        public static UnityEvent playerDead;

        [SerializeField] private float maxHP;
        [SerializeField] private float maxBullet;

        private float currentHP;

        private void Awake()
        {
            Player.playerProperty = this;
        }

        public void SetHP(float _amount)
        {
            currentHP -= _amount;

            if(currentHP <= 0)
            {
                playerDead?.Invoke();
            }
        }

        public GameObject GetPlayerObj()
        {
            return this.gameObject;
        }

    }
}
