using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float slideSpeed = 5f; // �p�h���̃X���C�h���x
    public float smoothTime = 0.01f; // �ړ��̊��炩���𐧌䂷��p�����[�^
    public float longPressDuration = 0.1f; // �������̎��ԁi�b�j

    private Camera mainCamera; // ���C���J����
    private float paddleWidth; // �p�h���̕�
    private Vector3 touchPosition; // �^�b�`�̈ʒu
    private Vector3 velocity = Vector3.zero; // �ړ��̑��x�x�N�g��
    private bool isTouching = false; // �^�b�`���s���Ă��邩�ǂ����̃t���O
    private bool isLongPressing = false; // ���������Ă��邩�ǂ����̃t���O
    private float longPressTimer = 0f; // �������̃^�C�}�[

    public int blinkCount = 3; // �_�ŉ�
    public float blinkDuration = 0.5f; // �_�ł̌p�����ԁi�b�j
    public float blinkInterval = 0.1f; // �_�ł̊Ԋu�i�b�j

    private Renderer objectRenderer;

    int maxHp = 10; // �ő�̗�
    int currentHp; //�@���݂̗̑�
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
        // �^�b�`���ꂽ�w�̐����擾
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            // �ŏ��Ɍ��o���ꂽ�^�b�`�̈ʒu���擾
            Touch touch = Input.GetTouch(0);

            if (!isTouching)
            {
                // �^�b�`���J�n���ꂽ�ꍇ�Ƀt���O�𗧂Ă�
                if (touch.phase == TouchPhase.Began)
                {
                    isTouching = true;
                    longPressTimer = 0f;
                }
            }
            else
            {
                // �^�b�`���I�������ꍇ�Ƀt���O��������
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isTouching = false;
                    isLongPressing = false;
                }

                // �^�b�`�ʒu�����[���h���W�ɕϊ�
                touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;

                // �p�h���̖ڕW�ʒu���v�Z
                float targetX = Mathf.Clamp(touchPosition.x, mainCamera.ScreenToWorldPoint(Vector3.zero).x + paddleWidth / 2f,
                    mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - paddleWidth / 2f);

                // �p�h�����X���[�Y�Ɉړ�������
                if (isLongPressing)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetX, transform.position.y, 0f), ref velocity, smoothTime);
                }

                // ����������
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
            Destroy(other.gameObject); // �Փ˂����I�u�W�F�N�g���폜����
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
            objectRenderer.enabled = false; // �I�u�W�F�N�g���\���ɂ���
            yield return new WaitForSeconds(blinkInterval);
            objectRenderer.enabled = true; // �I�u�W�F�N�g��\������
            yield return new WaitForSeconds(blinkInterval);
        }

        yield return new WaitForSeconds(blinkDuration - (blinkInterval * 2 * blinkCount));

        objectRenderer.enabled = true; // �I�u�W�F�N�g��\������
    }
}
