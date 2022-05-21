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
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, spd * Time.deltaTime);
    }
    public void Damage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Instantiate(deadPcy).transform.position = transform.position;
    }
   
}
