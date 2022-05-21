using UnityEngine;
public class Enemy : MonoBehaviour
{
    GameObject target;
    private float spd;
    [SerializeField] private float hp;
    [SerializeField] private ParticleSystem deadPcy;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        spd = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, spd * Time.deltaTime);
    }
    private void OnDestroy()
    {
        Instantiate(deadPcy).transform.position = transform.position;
    }
}
