using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    [Header("스코어, 연료, Hp, 무적, 파워")]
    public List<int> dropItem = new List<int>();
    public bool useEffect;
    public float Hp;
    public float time;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ItemDrop()
    {
        if (!useEffect) { return; }
        for(int i = 0; i < dropItem[0]; i++)
        {
            GameObject item = Instantiate(ObjectManager.Instance.scoreUp);
            item.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
        for (int i = 0; i < dropItem[1]; i++)
        {
            GameObject item = Instantiate(ObjectManager.Instance.fuelUp);
            item.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
        for (int i = 0; i < dropItem[2]; i++)
        {
            GameObject item = Instantiate(ObjectManager.Instance.hpUp);
            item.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
        for (int i = 0; i < dropItem[3]; i++)
        {
            GameObject item = Instantiate(ObjectManager.Instance.invincibility);
            item.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
        for (int i = 0; i < dropItem[4]; i++)
        {
            GameObject item = Instantiate(ObjectManager.Instance.powerUp);
            item.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
    }
    public void onDamage(int dmg)
    {
        Hp -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("resetSprite", 0.05f);
        if(Hp <= 0 && gameObject.name != "Boss")
        {
            Destroy(gameObject);
        }

    }
    public void resetSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            GameManager.Instance.score += 10;
            if(collision.gameObject.name != "Boom")
            {
                Destroy(collision.gameObject);
            }

            onDamage(collision.GetComponent<PlayerBullet>().dmg);
        }
    }
}
