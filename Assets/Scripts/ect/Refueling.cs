using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refueling : MonoBehaviour
{
    private static Refueling instance;
    Rigidbody2D rigid;
    public bool canUse;
    public float canGiveHp;
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
        rigid = GetComponent<Rigidbody2D>();
    }
    public static Refueling Instance
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
    IEnumerator refueling()
    {
        if (Player.Instance.isGameOver) { yield break; }
        if(canGiveHp <= 0) { yield break; }
        transform.position = new Vector3(5, -9, 0);
        canUse = false;
        Player.Instance.isRefueling = true;
        Player.Instance.canMove = false;
        Player.Instance.canAttack = false;
        rigid.velocity = ((Player.Instance.transform.position + Vector3.up * 0.3f) - transform.position).normalized * 10f;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, (Player.Instance.transform.position + Vector3.up * 0.3f)) <= 0.1f);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        rigid.velocity = Vector3.down * 0.2f;
        yield return new WaitForSeconds(1f);
        rigid.velocity = Vector3.zero;
        while(Player.Instance.curHp < Player.Instance.maxHp && canGiveHp >= 0)
        {
            canGiveHp -= 0.05f;
            Player.Instance.curHp += 0.05f;
            yield return new WaitForSeconds(0.02f);
        }
        canGiveHp = Mathf.Round(canGiveHp);
        Player.Instance.isRefueling = false;
        Player.Instance.canMove = true;
        Player.Instance.canAttack = true;
        rigid.velocity = new Vector3(-10, 20);
        yield return new WaitForSeconds(1f);
        rigid.velocity = Vector3.zero;
        canUse = true;
    }
}
