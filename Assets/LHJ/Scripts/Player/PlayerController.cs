using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private BulletObejctPool bullets;
        [SerializeField] private Transform firePos;

        public static BulletObejctPool staticBullets;
        private Rigidbody playerRG;
        private Vector3 velocity;
        private float inputX;
        private float inputZ;
        private float time;
        private float currentRotateY;

        private void Awake()
        {
            playerRG = GetComponent<Rigidbody>();
            staticBullets = bullets;

            Player.playerController = this;
            currentRotateY = transform.rotation.eulerAngles.y;
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

            if (velocity != Vector3.zero)
            {
                float angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;

                if (90.0f > angle && angle > -90.0f && angle != 0)
                {
                    transform.Rotate((angle / Mathf.Abs(angle)) * Vector3.up * rotateSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(velocity * speed * Time.deltaTime);
                }
            }

        }

   
        void Fire()
        {
            if (time < fireRate) return;

            //น฿ป็
            time -= fireRate;

            staticBullets.GetObj(firePos.position, transform.forward);
        }

        public float GetSpeed()
        {
            return speed;
        }
    }
}
