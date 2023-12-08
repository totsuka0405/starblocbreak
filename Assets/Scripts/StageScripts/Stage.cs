using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject parentObject; // ポジションの親オブジェクト
    public GameObject positionPrefab; // 生成するポジションのPrefab
    public float xMin = -2.4f; // x方向の最小値
    public float xMax = 2.4f; // x方向の最大値
    public int xCount = 9; // x方向の生成位置数
    public float yMin = 0f; // y方向の最小値
    public float yMax = 5.1f; // y方向の最大値
    public int yCount = 18; // y方向の生成位置数
    public GameObject enemyPrefab; // 敵のプレハブ
    public GameObject gray_enemyPrefab; // 敵のプレハブ
    public GameObject white_enemyPrefab; // 敵のプレハブ
    public GameObject sky_enemyPrefab; // 敵のプレハブ
    public GameObject red_enemyPrefab; // 敵のプレハブ
    public GameObject blue_enemyPrefab; // 敵のプレハブ
    public GameObject yellow_enemyPrefab; // 敵のプレハブ
    public GameObject orange_enemyPrefab; // 敵のプレハブ
    public GameObject purple_enemyPrefab; // 敵のプレハブ
    public GameObject green_enemyPrefab; // 敵のプレハブ
    public GameObject black_enemyPrefab; // 敵のプレハブ

    private Dictionary<string, Transform> positionDictionary = new Dictionary<string, Transform>();

    void Start()
    {
        GeneratePositions();
        TestSpawnEnemies();
    }

    void GeneratePositions()
    {
        float xInterval = (xMax - xMin) / (xCount - 1); // x方向の間隔
        float yInterval = (yMax - yMin) / (yCount - 1); // y方向の間隔

        int id = 1; // 初期ID

        for (int i = 0; i < xCount; i++)
        {
            for (int j = 0; j < yCount; j++)
            {
                float xPos = xMin + i * xInterval; // x座標の計算
                float yPos = yMin + j * yInterval; // y座標の計算

                Vector3 position = new Vector3(xPos, yPos, 0f);
                GameObject newPosition = Instantiate(positionPrefab, position, Quaternion.identity);
                newPosition.transform.parent = parentObject.transform;

                // ポジションにIDを設定する
                newPosition.name = "Position_" + id.ToString();

                // ポジションを辞書に追加する
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
