using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    private static Boss instance;
    public float maxHp;
    public bool useBoom;
    public int pageIndex;
    public int maxPageIndex;
    public bool isStartBoss;
    public int bossTimer;
    public GameObject bulletDestroy;
    public Coroutine curSpell;

    public List<GameObject> firePoints = new List<GameObject>();

    public Slider hpBar;
    public Text timer;

    public GameObject Explosion;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        dropItem[4] = Random.Range(1, 2);
        dropItem[3] = Random.Range(0, 1);
        switch (GameManager.Instance.stage)
        {
            case 1:
                maxHp = 750;
                maxPageIndex = 2;
                break;
            case 2:
                maxHp = 1250;
                maxPageIndex = 3;
                break;
            case 3:
                maxHp = 1500;
                maxPageIndex = 5;
                break;
            default:
                maxHp = 1000;
                break;
        }
        Hp = maxHp;
        StartCoroutine("BossManagement");
        StartCoroutine("BossTimer");
    }
    IEnumerator BossManagement()
    {
        while (true)
        {
            if (isStartBoss)
            {
                hpBar.gameObject.SetActive(true);
                hpBar.value = Hp / maxHp;
                if (Hp <= 0 || bossTimer <= 0)
                {
                    StopCoroutine(curSpell);
                    bulletDestroy.SetActive(true);
                    isStartBoss = false;
                    for(int i = 0; i < 5; i++)
                    {
                        firePoints[i].transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    pageIndex++;
                    if (!useBoom)
                    {
                        ItemDrop();
                    }
                    useBoom = false;
                    bossTimer = 60;
                    if(pageIndex > maxPageIndex)
                    {
                        isStartBoss = false;
                        hpBar.gameObject.SetActive(false);
                        timer.gameObject.SetActive(false);
                        if(GameManager.Instance.stage < 3)
                        {
                            GetComponent<Rigidbody2D>().velocity = Vector2.up * 1.5f;
                            yield return new WaitUntil(() => transform.position.y > 13);
                            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            GameManager.Instance.StageClear();
                        }
                        else
                        {
                            Explosion.SetActive(true);
                            transform.position = new Vector3(0, 15, 0);
                            yield return new WaitForSeconds(0.5f);
                            Explosion.SetActive(false);
                            yield return new WaitForSeconds(2f);
                            GameManager.Instance.GameClear();
                        }
                    }
                    else
                    {
                        Hp = maxHp;
                        yield return new WaitForSeconds(3f);
                        bulletDestroy.SetActive(false);
                        isStartBoss = true;
                        switch (pageIndex)
                        {
                            case 1:
                                StartCoroutine("Spell1");
                                break;
                            case 2:
                            case 4:
                                curSpell = StartCoroutine("BasicSpell");
                                break;
                            case 3:
                                curSpell = StartCoroutine("Spell3");
                                break;
                            case 5:
                                curSpell = StartCoroutine("Spell5");
                                break;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator BasicSpell()
    {
        while (true)
        {
            for(int i = 0; i < 4; i++)
            {
                GameObject bullet = Instantiate(ObjectManager.Instance.enemybullet1);
                bullet.transform.position = firePoints[i].transform.position;
                bullet.transform.localScale = new Vector3(4, 4, 1);
                bullet.GetComponent<Rigidbody2D>().AddForce((Player.Instance.transform.position - bullet.transform.position).normalized * 5, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Spell1()
    {
        while (true)
        {
            for(int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 30; i++)
                {
                    GameObject bullet = Instantiate(ObjectManager.Instance.enemybullet2);
                    bullet.transform.position = firePoints[4].transform.position;
                    bullet.transform.rotation = firePoints[4].transform.rotation;
                    bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 7, ForceMode2D.Impulse);

                    firePoints[4].transform.Rotate(Vector3.forward * (360 / 30));
                }
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.75f);
            for(int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject bullet = Instantiate(ObjectManager.Instance.enemybullet1);
                    bullet.transform.position = firePoints[i].transform.position;
                    bullet.GetComponent<Rigidbody2D>().AddForce((Player.Instance.transform.position - bullet.transform.position).normalized * 15, ForceMode2D.Impulse);
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Spell3()
    {
        while (true)
        {
            for(int k = 0; k < 6; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector3 dir = Player.Instance.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion angleAxis = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    firePoints[i].transform.rotation = angleAxis;
                    firePoints[i].transform.Rotate(Vector3.forward * 90);
                    for (int j = 0; j < 7; j++)
                    {
                        GameObject bullet = Instantiate(ObjectManager.Instance.enemybullet2);
                        bullet.transform.position = firePoints[i].transform.position;
                        bullet.transform.rotation = firePoints[i].transform.rotation;
                        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 5, ForceMode2D.Impulse);
                        firePoints[i].transform.Rotate(Vector3.forward * -30);
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator Spell5()
    {
        while (true)
        {
            for(int i = 0; i < 10; i++)
            {
                GameObject meteor = Instantiate(ObjectManager.Instance.meteor);
                meteor.transform.position = GameManager.Instance.spawnPoints[Random.Range(0, 19)];
                meteor.GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.3f, 0.7f);
                meteor.GetComponent<Enemy>().Hp = 20;
                GameManager.Instance.Enemys.Add(meteor);
                yield return new WaitForSeconds(Random.Range(0.3f,1f));
            }
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator BossTimer()
    {
        while (true)
        {
            if(isStartBoss && bossTimer > 0)
            {
                timer.gameObject.SetActive(true);
                bossTimer--;
                timer.text = bossTimer.ToString("D2");
            }
            yield return new WaitForSeconds(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet") && isStartBoss)
        {
                GameManager.Instance.score += 10;
            if(collision.gameObject.name != "Boom")
            {
                Destroy(collision.gameObject);
            }
                
                onDamage(collision.GetComponent<PlayerBullet>().dmg);
        }
    }
    public static Boss Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
}
