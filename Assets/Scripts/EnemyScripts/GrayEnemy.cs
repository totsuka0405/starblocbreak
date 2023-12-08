using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayEnemy : MonoBehaviour
{
    public int maxHp = 1; // 最大体力
    private int currentHp; // 現在の体力

    private void Start()
    {
        currentHp = maxHp; // 初期体力を設定する
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
}
