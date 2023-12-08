using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f;
    public float minSpeed = 5f;
    public float maxSpeed = 20f;

    public GameObject ballPrefab;
    private List<GameObject> balls = new List<GameObject>();

    Rigidbody myRigidbody;
    // Transformコンポーネントを保持しておくための変数を追加
    Transform myTransform;
    private Vector3 initialVelocity;

    private SpriteRenderer spriteRenderer;
    private int originalOrderInLayer;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        // Transformコンポーネントを取得して保持しておく
        myTransform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalOrderInLayer = spriteRenderer.sortingOrder;
        ApplyForceToBall();
    }

    void Update()
    {
        Vector3 velocity = myRigidbody.velocity;
        float clampedSpeed = Mathf.Clamp(velocity.magnitude, minSpeed, maxSpeed);
        myRigidbody.velocity = velocity.normalized * clampedSpeed;
    }

    // 衝突したときに呼ばれる
    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たったときに、跳ね返る方向を変える
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーの位置を取得
            Vector3 playerPos = collision.transform.position;
            // ボールの位置を取得
            Vector3 ballPos = myTransform.position;
            // プレイヤーから見たボールの方向を計算
            Vector3 direction = (ballPos - playerPos).normalized;
            // 現在の速さを取得
            float speed = myRigidbody.velocity.magnitude;
            // 速度を変更
            myRigidbody.velocity = direction * speed;
        }

        //下壁に当たった時の処理
        if (collision.gameObject.CompareTag("BottomWall"))
        {
            //ボールを非表示
            this.gameObject.SetActive(false);
        }
    }

    //ボールのスキル一覧
    //白ボール
    private void ApplyForceToBall()
    {
        initialVelocity = new Vector3(0f, speed, 0f);
        myRigidbody.velocity = initialVelocity;
    }

    //赤ボール
    public void SpeedUp()
    {
        speed += 1.0f;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        myRigidbody.velocity = myRigidbody.velocity.normalized * speed;
    }

    //空ボール
    public void ScaleUp()
    {
        myTransform.localScale = Vector3.one * 1.0f;
    }

    //青ボール
    public void Increased()
    {
        GameObject ball = Instantiate(ballPrefab, myTransform.position, myTransform.rotation);
        balls.Add(ball);
    }
}
