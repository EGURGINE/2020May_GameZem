using UnityEngine;
public abstract class Enemy : MonoBehaviour
{
    GameObject target;// 플레이어
    protected float spd; // 스피드
    [SerializeField] protected float hp; // 체력
    [SerializeField] protected ParticleSystem deadPcy; // 죽는 이펙트
    [SerializeField] protected float score;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player(Clone)");
        spd = Random.Range(1, 6);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x,0,target.transform.position.z));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, spd * Time.deltaTime);
    }
    public void Damage(float dmg)
    {
        SoundManager.Instance.PlaySound(Sound_Effect.HIT);
        hp -= dmg;
    }
}
