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
    // Transform�R���|�[�l���g��ێ����Ă������߂̕ϐ���ǉ�
    Transform myTransform;
    private Vector3 initialVelocity;

    private SpriteRenderer spriteRenderer;
    private int originalOrderInLayer;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        // Transform�R���|�[�l���g���擾���ĕێ����Ă���
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

    // �Փ˂����Ƃ��ɌĂ΂��
    void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɓ��������Ƃ��ɁA���˕Ԃ������ς���
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�̈ʒu���擾
            Vector3 playerPos = collision.transform.position;
            // �{�[���̈ʒu���擾
            Vector3 ballPos = myTransform.position;
            // �v���C���[���猩���{�[���̕������v�Z
            Vector3 direction = (ballPos - playerPos).normalized;
            // ���݂̑������擾
            float speed = myRigidbody.velocity.magnitude;
            // ���x��ύX
            myRigidbody.velocity = direction * speed;
        }

        //���ǂɓ����������̏���
        if (collision.gameObject.CompareTag("BottomWall"))
        {
            //�{�[�����\��
            this.gameObject.SetActive(false);
        }
    }

    //�{�[���̃X�L���ꗗ
    //���{�[��
    private void ApplyForceToBall()
    {
        initialVelocity = new Vector3(0f, speed, 0f);
        myRigidbody.velocity = initialVelocity;
    }

    //�ԃ{�[��
    public void SpeedUp()
    {
        speed += 1.0f;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        myRigidbody.velocity = myRigidbody.velocity.normalized * speed;
    }

    //��{�[��
    public void ScaleUp()
    {
        myTransform.localScale = Vector3.one * 1.0f;
    }

    //�{�[��
    public void Increased()
    {
        GameObject ball = Instantiate(ballPrefab, myTransform.position, myTransform.rotation);
        balls.Add(ball);
    }
}
