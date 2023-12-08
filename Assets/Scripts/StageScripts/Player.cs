using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float slideSpeed = 5f; // パドルのスライド速度
    public float smoothTime = 0.01f; // 移動の滑らかさを制御するパラメータ
    public float longPressDuration = 0.1f; // 長押しの時間（秒）

    private Camera mainCamera; // メインカメラ
    private float paddleWidth; // パドルの幅
    private Vector3 touchPosition; // タッチの位置
    private Vector3 velocity = Vector3.zero; // 移動の速度ベクトル
    private bool isTouching = false; // タッチが行われているかどうかのフラグ
    private bool isLongPressing = false; // 長押ししているかどうかのフラグ
    private float longPressTimer = 0f; // 長押しのタイマー

    public int blinkCount = 3; // 点滅回数
    public float blinkDuration = 0.5f; // 点滅の継続時間（秒）
    public float blinkInterval = 0.1f; // 点滅の間隔（秒）

    private Renderer objectRenderer;

    int maxHp = 10; // 最大体力
    int currentHp; //　現在の体力
    public Slider hp_slider;


    private void Start()
    {
        mainCamera = Camera.main;
        paddleWidth = GetComponent<Renderer>().bounds.size.x;
        objectRenderer = GetComponent<Renderer>();
        hp_slider.value = 1;
        currentHp = maxHp;
    }

    private void Update()
    {
        // タッチされた指の数を取得
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            // 最初に検出されたタッチの位置を取得
            Touch touch = Input.GetTouch(0);

            if (!isTouching)
            {
                // タッチが開始された場合にフラグを立てる
                if (touch.phase == TouchPhase.Began)
                {
                    isTouching = true;
                    longPressTimer = 0f;
                }
            }
            else
            {
                // タッチが終了した場合にフラグを下げる
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isTouching = false;
                    isLongPressing = false;
                }

                // タッチ位置をワールド座標に変換
                touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;

                // パドルの目標位置を計算
                float targetX = Mathf.Clamp(touchPosition.x, mainCamera.ScreenToWorldPoint(Vector3.zero).x + paddleWidth / 2f,
                    mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - paddleWidth / 2f);

                // パドルをスムーズに移動させる
                if (isLongPressing)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetX, transform.position.y, 0f), ref velocity, smoothTime);
                }

                // 長押し判定
                if (touch.phase == TouchPhase.Stationary)
                {
                    longPressTimer += Time.deltaTime;
                    if (longPressTimer >= longPressDuration)
                    {
                        isLongPressing = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // 衝突したオブジェクトを削除する
            StartCoroutine(BlinkCoroutine());
            int damage = 1;
            currentHp -= damage;
            hp_slider.value = (float)currentHp / (float)maxHp;
        }
    }
    private IEnumerator BlinkCoroutine()
    {

        for (int i = 0; i < blinkCount; i++)
        {
            objectRenderer.enabled = false; // オブジェクトを非表示にする
            yield return new WaitForSeconds(blinkInterval);
            objectRenderer.enabled = true; // オブジェクトを表示する
            yield return new WaitForSeconds(blinkInterval);
        }

        yield return new WaitForSeconds(blinkDuration - (blinkInterval * 2 * blinkCount));

        objectRenderer.enabled = true; // オブジェクトを表示する
    }
}
