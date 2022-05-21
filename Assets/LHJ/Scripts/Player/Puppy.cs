using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public interface ICollision
{
    public void Collide();
}


public class Puppy : MonoBehaviour, ICollision
{
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxHP = 100f;

    public static Action puppyDead;

    private float speed;
    private float currentHP;
    private Rigidbody puppyRigid;
    private GameObject target;

    private void Awake()
    {
        puppyRigid = GetComponent<Rigidbody>();
        currentHP = maxHP;
    }

    private void Start()
    {
        speed = Players.Player.playerController.GetSpeed() * 0.8f;
        target = Players.Player.playerProperty.GetPlayerObj();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(target.transform.position, transform.position) > minDistance)
            Move();
        else
        {
            puppyRigid.velocity = Vector3.zero;
        }
    }

    void Move()
    {   
        Vector3 dir = target.transform.position - transform.position;

        transform.DOLookAt(target.transform.position, 1f);
        //transform.position += dir * speed * Time.deltaTime;
        puppyRigid.velocity = transform.forward.normalized * speed;
    }

    public void SetHP(float _amount)
    {
        currentHP += _amount;
        if(currentHP <= 0)
        {
            puppyDead?.Invoke();
        }
    }

    public void Collide()
    {
        SetHP(-15f);
    }
}
