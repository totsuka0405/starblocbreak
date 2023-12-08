using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteEnemy : MonoBehaviour
{
    public float speed = 5f;
    public int maxHp = 5; // �ő�̗�
    private int currentHp; // ���݂̗̑�
    public GameObject whiteBullet;
    public Transform launchPoint; // ���̔��ˈʒu
    public float launchInterval = 5f; // ���̔��ˊԊu�i�b�j

    private float timer = 0f; // �o�ߎ���

    private void Start()
    {
        currentHp = maxHp; // �����̗͂�ݒ肷��
    }

    private void Update()
    {
        // �^�C�}�[���X�V
        timer += Time.deltaTime;

        // �w��̊Ԋu�ŋ��𔭎˂���
        if (timer >= launchInterval)
        {
            WhiteShot();
            timer = 0f; // �^�C�}�[�����Z�b�g
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>(); // GameManager���擾����i�K�XGameManager�̃N���X���ɕύX���Ă��������j

            if (gameManager != null)
            {
                // �{�[���̍U���͂��擾���A�G�̗̑͂������
                int ballAttackPower = gameManager.GetBallAttackPower();
                currentHp -= ballAttackPower;

                // �̗͂�0�ȉ��ɂȂ�����G���폜����
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
            GameManager gameManager = FindObjectOfType<GameManager>(); // GameManager���擾����i�K�XGameManager�̃N���X���ɕύX���Ă��������j

            if (gameManager != null)
            {
                // �{�[���̍U���͂��擾���A�G�̗̑͂������
                int ballAttackPower = gameManager.GetBallAttackPower();
                currentHp -= ballAttackPower;

                // �̗͂�0�ȉ��ɂȂ�����G���폜����
                if (currentHp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void WhiteShot()
    {
        // ���𐶐����Ĕ��˂���
        GameObject bullet = Instantiate(whiteBullet, launchPoint.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = new Vector3(0f, -speed, 0f);
        }
        Destroy(bullet, 3f); 
    }
}
