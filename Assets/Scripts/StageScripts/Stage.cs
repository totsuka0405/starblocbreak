using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject parentObject; // �|�W�V�����̐e�I�u�W�F�N�g
    public GameObject positionPrefab; // ��������|�W�V������Prefab
    public float xMin = -2.4f; // x�����̍ŏ��l
    public float xMax = 2.4f; // x�����̍ő�l
    public int xCount = 9; // x�����̐����ʒu��
    public float yMin = 0f; // y�����̍ŏ��l
    public float yMax = 5.1f; // y�����̍ő�l
    public int yCount = 18; // y�����̐����ʒu��
    public GameObject enemyPrefab; // �G�̃v���n�u
    public GameObject gray_enemyPrefab; // �G�̃v���n�u
    public GameObject white_enemyPrefab; // �G�̃v���n�u
    public GameObject sky_enemyPrefab; // �G�̃v���n�u
    public GameObject red_enemyPrefab; // �G�̃v���n�u
    public GameObject blue_enemyPrefab; // �G�̃v���n�u
    public GameObject yellow_enemyPrefab; // �G�̃v���n�u
    public GameObject orange_enemyPrefab; // �G�̃v���n�u
    public GameObject purple_enemyPrefab; // �G�̃v���n�u
    public GameObject green_enemyPrefab; // �G�̃v���n�u
    public GameObject black_enemyPrefab; // �G�̃v���n�u

    private Dictionary<string, Transform> positionDictionary = new Dictionary<string, Transform>();

    void Start()
    {
        GeneratePositions();
        TestSpawnEnemies();
    }

    void GeneratePositions()
    {
        float xInterval = (xMax - xMin) / (xCount - 1); // x�����̊Ԋu
        float yInterval = (yMax - yMin) / (yCount - 1); // y�����̊Ԋu

        int id = 1; // ����ID

        for (int i = 0; i < xCount; i++)
        {
            for (int j = 0; j < yCount; j++)
            {
                float xPos = xMin + i * xInterval; // x���W�̌v�Z
                float yPos = yMin + j * yInterval; // y���W�̌v�Z

                Vector3 position = new Vector3(xPos, yPos, 0f);
                GameObject newPosition = Instantiate(positionPrefab, position, Quaternion.identity);
                newPosition.transform.parent = parentObject.transform;

                // �|�W�V������ID��ݒ肷��
                newPosition.name = "Position_" + id.ToString();

                // �|�W�V�����������ɒǉ�����
                positionDictionary.Add(newPosition.name, newPosition.transform);

                id++;
            }
        }
    }

    void TestSpawnEnemies()
    {
        Transform position1 = FindPositionById("Position_1");
        Transform position20 = FindPositionById("Position_20");
        Transform position37 = FindPositionById("Position_37");

        if (position1 != null)
        {
            Vector3 position1Pos = position1.position;
            GameObject newEnemy1 = Instantiate(enemyPrefab, position1Pos, Quaternion.identity);
            newEnemy1.transform.parent = position1;
            newEnemy1.name = "Enemy_Position_1";
        }

        if (position20 != null)
        {
            Vector3 position20Pos = position20.position;
            GameObject newEnemy20 = Instantiate(enemyPrefab, position20Pos, Quaternion.identity);
            newEnemy20.transform.parent = position20;
            newEnemy20.name = "Enemy_Position_20";
        }

        if (position37 != null)
        {
            Vector3 position37Pos = position37.position;
            GameObject newEnemy37 = Instantiate(enemyPrefab, position37Pos, Quaternion.identity);
            newEnemy37.transform.parent = position37;
            newEnemy37.name = "Enemy_Position_37";
        }
    }

    void Stage1()
    {

    }

    Transform FindPositionById(string id)
    {
        if (positionDictionary.ContainsKey(id))
        {
            return positionDictionary[id];
        }

        return null;
    }
}
