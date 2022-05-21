using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float rotateSpeed = 5f;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private BulletObejctPool bullets;
        [SerializeField] private Transform firePos;

        public static BulletObejctPool staticBullets;
        private Rigidbody playerRG;
        private Vector3 velocity;
        private float inputX;
        private float inputZ;
        private float time;

        private void Awake()
        {
            playerRG = GetComponent<Rigidbody>();
            staticBullets = bullets;

            Player.playerController = this;
        }

        private void FixedUpdate()
        {
            time += Time.fixedDeltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }

            Move();
        }

        void Move()
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputZ = Input.GetAxisRaw("Vertical");

            velocity.x = inputX;
            velocity.y = 0;
            velocity.z = inputZ;

            velocity = velocity.normalized;

            //�̵� �ι��� ī�޶�, �÷��̾� �̵� ��ȹ ������ ����

        }

        void Fire()
        {
            if (time < fireRate) return;

            //�߻�
            time -= fireRate;

            staticBullets.GetObj(firePos.position, transform.forward);
        }

        public float GetSpeed()
        {
            return speed;
        }
    }
}
