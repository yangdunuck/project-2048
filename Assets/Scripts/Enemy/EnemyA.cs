using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy
{
    private void Start()
    {
        if(GameManager.Instance.stage == 3)
        {
            Hp *= 2;
        }
        StartCoroutine("Attack");
    }
    IEnumerator Attack()
    {
        while (true)
        {
            GameObject bullet = Instantiate(ObjectManager.Instance.enemybullet1);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().AddForce((Player.Instance.transform.position - transform.position).normalized * 5, ForceMode2D.Impulse);
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
