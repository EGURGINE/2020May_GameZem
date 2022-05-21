using UnityEngine;
public class Enemy : MonoBehaviour
{
    GameObject target;// �÷��̾�
    private float spd; // ���ǵ�
    [SerializeField] private float hp; // ü��
    [SerializeField] private ParticleSystem deadPcy; // �״� ����Ʈ

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        spd = Random.Range(1, 6);
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
}
