using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_2 : Enemy
{
    [SerializeField] GameObject miniSlime;
    public override void Update()
    {
        base.Update();

        if (hp <= 0)
        {
            Instantiate(deadPcy).transform.position = transform.position;
            Instantiate(miniSlime).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
