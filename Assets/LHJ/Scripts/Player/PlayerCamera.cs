using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform puppy;
        private Vector3 pos;

        [SerializeField] private float xRotation;
        [SerializeField] private float length;

        private void Awake()
        {
            puppy = transform.parent;
        }

        private void Update()
        {
            SetCameraPos();
        }

        void SetCameraPos()
        {
            pos.x = 0;
            pos.y = Mathf.Tan(xRotation * Mathf.Deg2Rad) * length;
            pos.z = -Mathf.Cos(xRotation * Mathf.Deg2Rad) * length;

            transform.localPosition = pos;
        }
    }
}
