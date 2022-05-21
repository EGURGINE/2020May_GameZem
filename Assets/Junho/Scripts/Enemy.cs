using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    GameObject target;// 플레이어
    private float spd; // 스피드
    [SerializeField] private float hp; // 체력
    [SerializeField] private ParticleSystem deadPcy; // 죽는 이펙트

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("Player");
        target = Players.Player.playerProperty.GetPlayerObj();
        spd = UnityEngine.Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Damage(1);
        }
        transform.LookAt(new Vector3(target.transform.position.x,0,target.transform.position.z));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, spd * Time.deltaTime);
    }
    public void Damage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Instantiate(deadPcy).transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    bool damaging;

    public void OnCollisionStay(Collision collision)
    {
        ICollision icollision = collision.transform.GetComponent<ICollision>();
        if(icollision != null)
        {
            StartCoroutine(GetDamage(icollision, 1f));
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        damaging = false;
    }

    public IEnumerator GetDamage(ICollision _collision, float _time)
    {
        if (damaging) yield break;
        
        damaging = true;

        while(true)
        {
            _collision.Collide();
            yield return new WaitForSeconds(_time);
            Debug.Log("damaging");
            if (!damaging) yield break;
        }
    }
}
