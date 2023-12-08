using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayEnemy : MonoBehaviour
{
    public int maxHp = 1; // �ő�̗�
    private int currentHp; // ���݂̗̑�

    private void Start()
    {
        currentHp = maxHp; // �����̗͂�ݒ肷��
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
}
