using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float attack;

    protected Rigidbody bulletRG;
    private BulletObejctPool bullets;

    private void Awake()
    {
        bulletRG = transform.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        bulletRG.velocity = bulletRG.transform.forward.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Players.PlayerController.staticBullets.Return(this);
    }

}
