using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy
{
    private void Start()
    {
        if (GameManager.Instance.stage == 3)
        {
            Hp *= 2;
        }
        StartCoroutine("Attack");
    }
    IEnumerator Attack()
    {
        while (true)
        {
            GameObject bulletL = Instantiate(ObjectManager.Instance.enemybullet1);
            GameObject bulletR = Instantiate(ObjectManager.Instance.enemybullet1);
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            bulletL.GetComponent<Rigidbody2D>().AddForce((Player.Instance.transform.position - transform.position).normalized * 7.5f, ForceMode2D.Impulse);
            bulletR.GetComponent<Rigidbody2D>().AddForce((Player.Instance.transform.position - transform.position).normalized * 7.5f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1.5f);
        }
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
