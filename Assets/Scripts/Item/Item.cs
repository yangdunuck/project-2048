using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public virtual void GiveEffect()
    {
        Debug.Log("æ∆¿Ã≈€ »πµÊ");
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if(((Vector3.Distance(transform.position, Player.Instance.transform.position) <= 1 && Input.GetKey(KeyCode.LeftShift)) || Player.Instance.transform.position.y > 2) && !Player.Instance.isGameOver)
        {
            rigid.velocity = (Player.Instance.transform.position - transform.position).normalized * 10;
        }
        else
        {
            rigid.velocity = Vector2.down * 3;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !Player.Instance.isGameOver)
        {
            GiveEffect();
            Destroy(gameObject);
        }
    }
}
