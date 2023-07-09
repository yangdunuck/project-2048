using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    AudioSource audioSource;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public AudioClip[] audios;

    public Text msg;
    private static Player instance;

    public float speed;
    public float highSpeed;
    public float lowSpeed;
    public bool canMove;

    public float curHp;
    public float maxHp;

    public float curFuel;
    public float maxFuel;

    public bool isAttackDelay;
    public bool canAttack;
    public int power;

    public float invincibilityTime;

    public bool isRefueling;
    [SerializeField] GameObject boom;

    Vector3 dir;

    public float healCooltime;
    public float curHealCooltime;

    public bool isBoomCooltime;

    public bool isGameOver;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("StartHealCooltime");
        StartCoroutine("useFuel");
        msg.color = new Color(1, 1, 1, 0);
    }
    private void Update()
    {
        if (!isGameOver)
        {
            Move();
            Attack();
            Boom();
            Invincibility();
            Heal();
            if (Input.GetKeyDown(KeyCode.Space) && canMove && dir != Vector3.zero)
            {
                StartCoroutine("Dash");
            }
            if (invincibilityTime > 0)
            {
                invincibilityTime -= Time.deltaTime;
            }
        }
    }
    void Heal()
    {
        if (!Input.GetKeyDown(KeyCode.C)) { return; }
        if (curHealCooltime < healCooltime) { StartCoroutine("CantUse"); return; }
        if (!Refueling.Instance.canUse) { StartCoroutine("CantUse"); return;}
        curHealCooltime = 0;
        Refueling.Instance.StartCoroutine("refueling");
    }
    void Invincibility()
    {
        if (isRefueling)
        {
            gameObject.layer = 7;
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            if(invincibilityTime > 0)
            {
                gameObject.layer = 7;
                spriteRenderer.color = new Color(1, 1, 1, 0.3f);
            }
            else
            {
                gameObject.layer = 6;
                spriteRenderer.color = new Color(1, 1, 1, 1f);
            }
        }
    }
    void Boom()
    {
        if (!Input.GetKeyDown(KeyCode.X)) { return; }
        if(power <= 1) {StartCoroutine("CantUse"); return; }
        if (boom.activeSelf) {StartCoroutine("CantUse"); return; }
        if (!canAttack) {StartCoroutine("CantUse"); return; }
        if (isBoomCooltime) {StartCoroutine("CantUse"); return; }
        isBoomCooltime = true;
        StartCoroutine("BoomCooltime");
        power--;
        boom.SetActive(true);
        try
        {
            if (Boss.Instance.isStartBoss)
            {
                Boss.Instance.useBoom = true;
            }
        }
        catch { }
        Invoke("EndBoom", 1);
    }
    void EndBoom()
    {
        boom.SetActive(false);
    }
    void Attack()
    {
        if (!Input.GetKey(KeyCode.Z)) { return; }
        if (isAttackDelay) { return; }
        if (!canAttack) { return; }
        isAttackDelay = true;
        StartCoroutine("AttackDelay");
        switch (power)
        {
            case 1:
                GameObject bulletL = Instantiate(ObjectManager.Instance.playerBullet1);
                GameObject bulletR = Instantiate(ObjectManager.Instance.playerBullet1);
                bulletL.transform.position = transform.position + (Vector3.left * 0.15f);
                bulletR.transform.position = transform.position + (Vector3.right * 0.15f);
                bulletL.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                bulletR.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletLL = Instantiate(ObjectManager.Instance.playerBullet1);
                GameObject bulletRR = Instantiate(ObjectManager.Instance.playerBullet1);
                GameObject bulletMM = Instantiate(ObjectManager.Instance.playerBullet2);
                bulletLL.transform.position = transform.position + (Vector3.left * 0.3f);
                bulletRR.transform.position = transform.position + (Vector3.right * 0.3f);
                bulletMM.transform.position = transform.position;
                bulletLL.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                bulletRR.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                bulletMM.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletLLL = Instantiate(ObjectManager.Instance.playerBullet1);
                GameObject bulletRRR = Instantiate(ObjectManager.Instance.playerBullet1);
                GameObject bulletMMM = Instantiate(ObjectManager.Instance.playerBullet2);
                bulletLLL.transform.position = transform.position + (Vector3.left * 0.3f);
                bulletRRR.transform.position = transform.position + (Vector3.right * 0.3f);
                bulletMMM.transform.position = transform.position;
                bulletLLL.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                bulletRRR.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                bulletMMM.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                for(int i = 0; i < 2; i++)
                {
                    GameObject bullet3 = Instantiate(ObjectManager.Instance.playerBullet3);
                    bullet3.transform.position = transform.position;
                    if(i == 0)
                    {
                        bullet3.transform.Rotate(Vector3.forward * 45);
                    }
                    else
                    {
                        bullet3.transform.Rotate(Vector3.forward * -45);
                    }
                }
                break;
            case 4:
                GameObject bulletLLLL = Instantiate(ObjectManager.Instance.playerBullet1);
                GameObject bulletRRRR = Instantiate(ObjectManager.Instance.playerBullet1);
                GameObject bulletMMMM = Instantiate(ObjectManager.Instance.playerBullet2);
                bulletLLLL.transform.position = transform.position + (Vector3.left * 0.3f);
                bulletRRRR.transform.position = transform.position + (Vector3.right * 0.3f);
                bulletMMMM.transform.position = transform.position;
                bulletLLLL.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                bulletRRRR.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                bulletMMMM.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                for (int i = -2; i < 3; i++)
                {
                    if(i != 0)
                    {
                        GameObject bullet3 = Instantiate(ObjectManager.Instance.playerBullet3);
                        bullet3.transform.position = transform.position;
                        bullet3.transform.Rotate(Vector3.forward * i * 45);
                    }
                }
                break;
        }
    }
    void Move()
    {
        if (!canMove) { anim.SetInteger("Move", 0); return; }
        if (Input.GetKey(KeyCode.LeftShift)) { speed = lowSpeed; }
        else { speed = highSpeed; }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        dir = new Vector3(h, v, 0).normalized;

        transform.Translate(dir * speed * Time.deltaTime);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x > 1) { pos.x = 1; }
        if (pos.x < 0) { pos.x = 0; }
        if (pos.y > 1) { pos.y = 1; }
        if (pos.y < 0) { pos.y = 0; }

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Move", (int)h);
        }
    }
    void OnDamage()
    {
        curHp--;
        curHp = Mathf.Round(curHp);
        if (power > 1)
        {
            power--;
            GameObject powerItem = Instantiate(ObjectManager.Instance.powerUp);
            powerItem.transform.position = new Vector3(0, 4.5f, 0);
        }
        if (invincibilityTime <= 1)
        {
            invincibilityTime = 1;
        }
        if(curHp <= 0)
        {
            if (!isGameOver)
            {
                isGameOver = true;
                GameManager.Instance.StartCoroutine("GameOver");
            }
        }
        audioSource.clip = audios[0];
        audioSource.Play();
    }
    IEnumerator BoomCooltime()
    {
        yield return new WaitForSeconds(5f);
        isBoomCooltime = false;
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.2f);
        isAttackDelay = false;
    }
    IEnumerator Dash()
    {
        audioSource.clip = audios[1];
        audioSource.Play();
        curFuel -= 10;
        GetComponent<Rigidbody2D>().AddForce(dir * 30, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    IEnumerator StartHealCooltime()
    {
        while (true)
        {
            if(curHealCooltime < healCooltime)
            {
                curHealCooltime++;
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator useFuel()
    {
        while (true)
        {
            if (!isGameOver)
            {
                curFuel -= 0.035f;
                if (curFuel <= 0)
                {
                    isGameOver = true;
                    GameManager.Instance.StartCoroutine("GameOver");
                }
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator CantUse()
    {
        msg.color = new Color(1, 1, 1, 1);
        while(msg.color.a > 0)
        {
            msg.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.04f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet")) && !isGameOver)
        {
            OnDamage();
        }
    }
    public static Player Instance
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
