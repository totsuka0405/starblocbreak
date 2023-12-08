using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteEnemy : MonoBehaviour
{
    public float speed = 5f;
    public int maxHp = 5; // 最大体力
    private int currentHp; // 現在の体力
    public GameObject whiteBullet;
    public Transform launchPoint; // 球の発射位置
    public float launchInterval = 5f; // 球の発射間隔（秒）

    private float timer = 0f; // 経過時間

    private void Start()
    {
        currentHp = maxHp; // 初期体力を設定する
    }

    private void Update()
    {
        // タイマーを更新
        timer += Time.deltaTime;

        // 指定の間隔で球を発射する
        if (timer >= launchInterval)
        {
            WhiteShot();
            timer = 0f; // タイマーをリセット
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>(); // GameManagerを取得する（適宜GameManagerのクラス名に変更してください）

            if (gameManager != null)
            {
                // ボールの攻撃力を取得し、敵の体力から引く
                int ballAttackPower = gameManager.GetBallAttackPower();
                currentHp -= ballAttackPower;

                // 体力が0以下になったら敵を削除する
                if (currentHp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>(); // GameManagerを取得する（適宜GameManagerのクラス名に変更してください）

            if (gameManager != null)
            {
                // ボールの攻撃力を取得し、敵の体力から引く
                int ballAttackPower = gameManager.GetBallAttackPower();
                currentHp -= ballAttackPower;

                // 体力が0以下になったら敵を削除する
                if (currentHp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void WhiteShot()
    {
        // 球を生成して発射する
        GameObject bullet = Instantiate(whiteBullet, launchPoint.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = new Vector3(0f, -speed, 0f);
        }
        Destroy(bullet, 3f); 
    }
}
