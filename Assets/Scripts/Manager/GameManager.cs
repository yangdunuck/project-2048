using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public List<GameObject> Enemys;
    public int score;
    public int playtime;
    public int stage;

    public GameObject boss;

    public List<Vector3> spawnPoints = new List<Vector3>();
    public Image pauseSet;

    public Image GameOverSet;
    public Text ItemLine;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1;
        StartCoroutine("PlaytimeTimer");
        LoadData();
    }
    private void Start()
    {
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnEnemyC");
        StartCoroutine("SpawnMeteor");
        StartCoroutine("StartBoss");
        StartCoroutine("deleteItemLine");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    IEnumerator deleteItemLine()
    {
        yield return new WaitForSeconds(1f);
        ItemLine.color = new Color(1, 1, 1, 0);
    }
    void LoadData()
    {
        if(DataManager.Instance.stage == 0) { return; }
        Player.Instance.curHp = DataManager.Instance.hp;
        Player.Instance.curFuel = DataManager.Instance.fuel;
        Player.Instance.power = DataManager.Instance.power;
        stage = DataManager.Instance.stage;
        playtime = DataManager.Instance.playtime;
        score = DataManager.Instance.score;
    }
    public IEnumerator GameOver()
    {
        
        try
        {
            for (int i = 0; i < Enemys.Count; i++)
            {
                Enemys[i].GetComponent<Enemy>().useEffect = false;
            }
        }
        catch
        {

        }
        StopCoroutine("SpawnEnemy");
        StopCoroutine("SpawnEnemyC");
        StopCoroutine("SpawnMeteor");
        StopCoroutine("StartBoss");
        Player.Instance.canMove = false;
        Player.Instance.canAttack = false;
        Refueling.Instance.canUse = false;
        DataManager.Instance.hp = Player.Instance.curHp;
        DataManager.Instance.fuel = Player.Instance.curFuel;
        DataManager.Instance.stage = stage + 1;
        DataManager.Instance.power = Player.Instance.power;
        DataManager.Instance.playtime = playtime;
        DataManager.Instance.score = score;
        RankManager.Instance.RankSet(DataManager.Instance.score, DataManager.Instance.stage - 1, DataManager.Instance.nickname);
        while (Player.Instance.transform.localScale.x >= 0.1f)
        {
            Debug.Log("asdf");
            Player.Instance.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 2;
            Player.Instance.transform.localScale -= new Vector3(0.3f, 0.3f, 0);
            yield return new WaitForSeconds(0.2f);
        }
        SceneManager.LoadScene(8);
    }
    IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(180);
        StopCoroutine("SpawnEnemy");
        StopCoroutine("SpawnEnemyC");
        StopCoroutine("SpawnMeteor");
        yield return new WaitForSeconds(3f);
        boss.SetActive(true);
        boss.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1.5f;
        yield return new WaitUntil(() => boss.transform.position.y <= 4.5f);
        boss.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Boss.Instance.curSpell = Boss.Instance.StartCoroutine("Spell1");
        Enemys.Add(boss);
        Boss.Instance.isStartBoss = true;
    }
    public void Pause()
    {
        if (!Player.Instance.isGameOver)
        {
            if (Time.timeScale == 1)
            {
                pauseSet.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseSet.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    public void StageClear()
    {
        try
        {
            for (int i = 0; i < Enemys.Count; i++)
            {
                Enemys[i].GetComponent<Enemy>().useEffect = false;
            }
        }
        catch
        {

        }
        DataManager.Instance.hp = Player.Instance.curHp;
        DataManager.Instance.fuel = Player.Instance.curFuel;
        DataManager.Instance.stage = stage + 1;
        DataManager.Instance.power = Player.Instance.power;
        DataManager.Instance.playtime = playtime;
        DataManager.Instance.score = score;
        RankManager.Instance.RankSet(DataManager.Instance.score, DataManager.Instance.stage - 1, DataManager.Instance.nickname);
        SceneManager.LoadScene(5);
    }
    public void GameClear()
    {
        try
        {
            for(int i = 0; i < Enemys.Count; i++)
            {
                Enemys[i].GetComponent<Enemy>().useEffect = false;
            }
        }
        catch
        {

        }
        DataManager.Instance.hp = Player.Instance.curHp;
        DataManager.Instance.fuel = Player.Instance.curFuel;
        DataManager.Instance.stage = stage + 1;
        DataManager.Instance.power = Player.Instance.power;
        DataManager.Instance.playtime = playtime;
        DataManager.Instance.score = score;
        RankManager.Instance.RankSet(DataManager.Instance.score, DataManager.Instance.stage, DataManager.Instance.nickname);
        SceneManager.LoadScene(7);
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int enemyType = Random.Range(0, 3);
            int spawnPointNum = Random.Range(0, 23);
            float enemySpeed;
            GameObject enemy;
            if(enemyType == 0)
            {
                enemySpeed = 5;
                enemy = Instantiate(ObjectManager.Instance.enemyB);
            }
            else
            {
                enemySpeed = 3f;
                enemy = Instantiate(ObjectManager.Instance.enemyA);
            }
            enemy.transform.position = spawnPoints[spawnPointNum];
            if(spawnPointNum == 19 || spawnPointNum == 20)
            {
                enemy.transform.Rotate(Vector3.forward * -65);
            }
            else if (spawnPointNum == 21 || spawnPointNum == 22)
            {
                enemy.transform.Rotate(Vector3.forward * 65);
            }
            enemy.GetComponent<Rigidbody2D>().velocity = -enemy.transform.up * enemySpeed;
            float spawnDelay;
            switch (stage)
            {
                case 1:
                    spawnDelay = Random.Range(3f, 5f);
                    break;
                case 2:
                    spawnDelay = Random.Range(2f, 3f);
                    break;
                case 3:
                    spawnDelay = Random.Range(2f, 3f);
                    break;
                default:
                    Debug.LogError("스테이지가 범위를 벗어남");
                    spawnDelay = 500;
                    break;
            }
            Enemys.Add(enemy);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    IEnumerator SpawnEnemyC()
    {
        float spawnDelay;
        switch (stage)
        {
            case 1:
                spawnDelay = 47f;
                break;
            case 2:
                spawnDelay = 32f;
                break;
            case 3:
                spawnDelay = 30f;
                break;
            default:
                Debug.LogError("스테이지가 범위를 벗어남");
                spawnDelay = 500;
                break;
        }
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            GameObject enemy1 = Instantiate(ObjectManager.Instance.enemyC);
            GameObject enemy2 = Instantiate(ObjectManager.Instance.enemyC);
            enemy1.transform.position = spawnPoints[5];
            enemy2.transform.position = spawnPoints[15];
            Enemys.Add(enemy1);
            Enemys.Add(enemy2);
        }
    }
    IEnumerator SpawnMeteor()
    {
        yield return new WaitForSeconds(10);
        while (true)
        {
            float spawnDelay;
            switch (stage)
            {
                case 1:
                    spawnDelay = 47f + Random.Range(-3f, 3f);
                    break;
                case 2:
                    spawnDelay = 32f + Random.Range(-3f, 3f);
                    break;
                case 3:
                    spawnDelay = 17f + Random.Range(-3f, 3f);
                    break;
                default:
                    spawnDelay = 47f + Random.Range(-3f, 3f); ;
                    break;
            }
            yield return new WaitForSeconds(spawnDelay);
            GameObject meteor = Instantiate(ObjectManager.Instance.meteor);
            meteor.transform.position = spawnPoints[Random.Range(0, 19)];
            Enemys.Add(meteor);
        }
    }
    IEnumerator PlaytimeTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playtime++;
        }
    }
    public static GameManager Instance
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
