using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : Enemy
{
    Rigidbody2D rigid;
    public GameObject FirePoint;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine("Move");
    }
    IEnumerator Attack()
    {
        float attackDelay;
        switch (GameManager.Instance.stage)
        {
            case 1:
                attackDelay = 5f;
                break;
            case 2:
                attackDelay = 3.5f;
                break;
            case 3:
                attackDelay = 2;
                break;
            default:
                attackDelay = 5f;
                break;
        }
        while (true)
        {
            for(int i = 0; i < 25; i++)
            {
                GameObject bullet = Instantiate(ObjectManager.Instance.enemybullet2);
                bullet.transform.position = FirePoint.transform.position;
                bullet.transform.rotation = FirePoint.transform.rotation;
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 7.5f, ForceMode2D.Impulse);
                FirePoint.transform.Rotate(Vector3.forward * (360 / 25));
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }
    IEnumerator Move()
    {
        rigid.velocity = Vector2.down * 3;
        yield return new WaitForSeconds(1.5f);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        StartCoroutine("Attack");
        yield return new WaitForSeconds(10f);
        StopCoroutine("Attack");
        yield return new WaitForSeconds(1f);
        rigid.velocity = Vector2.down * 3;
    }
    private void OnDestroy()
    {
        try
        {
            ItemDrop();
        }
        catch { }
        try
        {
            GameManager.Instance.Enemys.Remove(gameObject);
        }
        catch { }
    }
}
