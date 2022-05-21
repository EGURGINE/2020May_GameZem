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
        
        private Rigidbody playerRG;
        private Vector3 velocity;
        private float inputX;
        private float inputZ;
        

        private float currentRotateY;

        private void Awake()
        {
            playerRG = GetComponent<Rigidbody>();
            

            Player.playerController = this;
            currentRotateY = transform.rotation.eulerAngles.y;            
        }

        private void FixedUpdate()
        {
            Move();
        }

        float xRotate = 0;

        void Move()
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputZ = Input.GetAxisRaw("Vertical");
            
            float yRotateSize = Input.GetAxis("MouseX") * rotateSpeed;
            float yRotate = transform.eulerAngles.y + yRotateSize;

            float xRotateSize = -Input.GetAxis("Mouse Y") * rotateSpeed;
            xRotate = Mathf.Clamp(xRotate + xRotateSize, -30, 30);

            velocity.x = inputX;
            velocity.y = 0;
            velocity.z = inputZ;

            if(velocity != Vector3.zero)
            {
                transform.Translate(velocity * speed * Time.deltaTime);
            }

            transform.localEulerAngles = new Vector3(0, yRotate, 0);
        }


        public float GetSpeed()
        {
            return speed;
        }
    }
}
