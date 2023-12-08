using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameManager : MonoBehaviour
{
    //ボールのprefab
    public GameObject ballPrefab;
    //ボールの生成位置
    [SerializeField]public Transform spawnPoint;

    //ボールのprefabをリスト化？　※イマイチわからない
    private List<GameObject> balls = new List<GameObject>();

    //スキル一覧
    //スキル回数
    private int whiteSkillCount;
    private int skySkillCount;
    private int redSkillCount;
    private int blueSkillCount;
    private int yellowSkillCount;
    //スキルテキスト
    public Text whiteSkillText;
    public Text skySkillText;
    public Text redSkillText;
    public Text blueSkillText;
    public Text yellowSkillText;
    // ボールの攻撃力
    public int ballAttackPower = 1;

    public void Start()
    {
        whiteSkillCount = 3;
        skySkillCount = 1;
        redSkillCount = 1;
        blueSkillCount = 1;
        yellowSkillCount = 1;
        spawnPoint = GameObject.FindGameObjectWithTag("Player").transform.Find("BallSpawn");
    }

    public void Update()
    {
        whiteSkillText.text     = "×" + whiteSkillCount;
        skySkillText.text       = "×" + skySkillCount;
        redSkillText.text       = "×" + redSkillCount;
        blueSkillText.text      = "×" + blueSkillCount;
        yellowSkillText.text    = "×" + yellowSkillCount;
    }

    //プレイヤーステータス
    //ボールの攻撃力
    public int GetBallAttackPower()
    {
        return ballAttackPower;
    }

    //スキル一覧
    //白スキル：射出
    public void SpawnObject()
    {
        if (whiteSkillCount >= 1)
        {
            GameObject ball = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
            balls.Add(ball);
            if(whiteSkillCount >= 1)
            {
                whiteSkillCount -= 1;
            }
        }
    }
    //赤スキル：速度UP
    public void SpeedUpAllBalls()
    {
        if (redSkillCount >= 1)
        {
            foreach (GameObject ball in balls)
            {
                Ball ballScript = ball.GetComponent<Ball>();
                ballScript.SpeedUp();
                if (redSkillCount >= 1)
                {
                    redSkillCount -= 1;
                }
            }
        }
    }
    //空スキル：サイズUP
    public void ScaleUpAllBalls()
    {
        if (skySkillCount >= 1)
        {
            foreach (GameObject ball in balls)
            {
                Ball ballScript = ball.GetComponent<Ball>();
                ballScript.ScaleUp();
                if (skySkillCount >= 1)
                {
                    skySkillCount -= 1;
                }
            }
        }
    }
    //青スキル：増殖
    public void IncreasedBalls()
    {
        if (blueSkillCount >= 1)
        {
            foreach (GameObject ball in balls)
            {
                Ball ballScript = ball.GetComponent<Ball>();
                ballScript.Increased();
                if (blueSkillCount >= 1)
                {
                    blueSkillCount -= 1;
                }
            }
        }
    }
    //黄スキル：貫通
    public void PenetrateBalls()
    {
        if (yellowSkillCount >= 1)
        {
            if (ballPrefab.activeSelf) // オブジェクトがアクティブであるかを追加のチェック条件にする
            {
                StartCoroutine(EnableBlockColliders());
                if (yellowSkillCount >= 1)
                {
                    yellowSkillCount -= 1;
                }
            }
        }
    }
    private IEnumerator EnableBlockColliders()
    {
        float duration = 3f; // 有効化する時間（秒）

        // BlockタグのついたオブジェクトのColliderを取得
        Collider[] blockColliders = GameObject.FindGameObjectsWithTag("Block")
            .Select(go => go.GetComponent<Collider>())
            .ToArray();

        // ColliderのisTriggerを有効にする
        foreach (Collider collider in blockColliders)
        {
            collider.isTrigger = true;
        }

        yield return new WaitForSeconds(duration);

        // ColliderのisTriggerを元に戻す
        foreach (Collider collider in blockColliders)
        {
            collider.isTrigger = false;
        }
    }
}
