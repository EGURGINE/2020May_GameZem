using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] monsters; //�� ����
    float spawnDelay = 0;
    float cnt;
    private void Update()
    {
        cnt += Time.deltaTime;
        if (cnt>=spawnDelay)
        {
            int spawnNum = Random.Range(1, 4); // �ѹ��� �� ��� ��ȯ����

            for (int i = 0; i < spawnNum; i++)
            {
                Instantiate(monsters[Random.Range(0, monsters.Length)]).transform.position = new Vector3(
                transform.position.x + Random.Range(-20, 20), 0, transform.position.z + Random.Range(-20, 20));
            }
            cnt = 0;
            spawnDelay = Random.Range(1,3);
        }
    }
}
