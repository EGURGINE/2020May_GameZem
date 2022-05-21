using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlime : Enemy
{
    private float cnt;
    [SerializeField] GameObject mediumSlime;
    public override void Update()
    {
        base.Update();
        cnt += Time.deltaTime;
        if (cnt>=5)
        {
            Instantiate(mediumSlime).transform.position = transform.position;
            Destroy(gameObject);
        }
        if (hp <= 0)
        {
            Instantiate(deadPcy).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
