using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameManager : MonoBehaviour
{
    //�{�[����prefab
    public GameObject ballPrefab;
    //�{�[���̐����ʒu
    [SerializeField]public Transform spawnPoint;

    //�{�[����prefab�����X�g���H�@���C�}�C�`�킩��Ȃ�
    private List<GameObject> balls = new List<GameObject>();

    //�X�L���ꗗ
    //�X�L����
    private int whiteSkillCount;
    private int skySkillCount;
    private int redSkillCount;
    private int blueSkillCount;
    private int yellowSkillCount;
    //�X�L���e�L�X�g
    public Text whiteSkillText;
    public Text skySkillText;
    public Text redSkillText;
    public Text blueSkillText;
    public Text yellowSkillText;
    // �{�[���̍U����
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
        whiteSkillText.text     = "�~" + whiteSkillCount;
        skySkillText.text       = "�~" + skySkillCount;
        redSkillText.text       = "�~" + redSkillCount;
        blueSkillText.text      = "�~" + blueSkillCount;
        yellowSkillText.text    = "�~" + yellowSkillCount;
    }

    //�v���C���[�X�e�[�^�X
    //�{�[���̍U����
    public int GetBallAttackPower()
    {
        return ballAttackPower;
    }

    //�X�L���ꗗ
    //���X�L���F�ˏo
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
    //�ԃX�L���F���xUP
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
    //��X�L���F�T�C�YUP
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
    //�X�L���F���B
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
    //���X�L���F�ђ�
    public void PenetrateBalls()
    {
        if (yellowSkillCount >= 1)
        {
            if (ballPrefab.activeSelf) // �I�u�W�F�N�g���A�N�e�B�u�ł��邩��ǉ��̃`�F�b�N�����ɂ���
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
        float duration = 3f; // �L�������鎞�ԁi�b�j

        // Block�^�O�̂����I�u�W�F�N�g��Collider���擾
        Collider[] blockColliders = GameObject.FindGameObjectsWithTag("Block")
            .Select(go => go.GetComponent<Collider>())
            .ToArray();

        // Collider��isTrigger��L���ɂ���
        foreach (Collider collider in blockColliders)
        {
            collider.isTrigger = true;
        }

        yield return new WaitForSeconds(duration);

        // Collider��isTrigger�����ɖ߂�
        foreach (Collider collider in blockColliders)
        {
            collider.isTrigger = false;
        }
    }
}
