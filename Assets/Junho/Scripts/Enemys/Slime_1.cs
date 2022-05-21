public class Slime_1 : Enemy
{
    public override void Update()
    {
        base.Update();

        if (hp <= 0)
        {
            Instantiate(deadPcy).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
