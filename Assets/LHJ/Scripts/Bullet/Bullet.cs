using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float attack;

    protected Rigidbody bulletRG;
    private BulletObejctPool bullets;
    Vector3 startPos;

    private void Awake()
    {
        bulletRG = transform.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        bulletRG.velocity = bulletRG.transform.forward.normalized * speed;
        startPos = transform.position;
    }

    private void Update()
    {
        if(Vector3.Distance(startPos, transform.position) > 100f)
        {
            Players.PlayerProperty.staticBullets.Return(this);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        
        enemy?.Damage(attack);

        Players.PlayerProperty.staticBullets.Return(this);
    }

}
