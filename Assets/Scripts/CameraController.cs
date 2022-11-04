using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform playerDiePos;

    [SerializeField] float defPosY;//�W����Y���W
    [SerializeField] float defPosZ;//�W����Z���W
    [SerializeField] float criteriaPro;//��̊���
    [SerializeField] float maxCriteriaPro;//�ő��̊���
    [SerializeField] float maxRange;//�ő�̉��s
    [SerializeField] float maxHeight;//�ő�̃J�����̍���
    [SerializeField] float criteriaZ;//���Z���W

    
    [SerializeField] Transform overPosition;
    [SerializeField] Transform clearPosition;
    [SerializeField] float range = 20f;//���͈̔�
    [SerializeField] float moveSpeed = 0.1f;//�������X�s�[�h
    private Vector3 defPos;//�����ʒu
    private Quaternion defRot;//�����ʒu
    private float screenHeigth;//�X�N���[���̍���
    private Vector3 distance;//Player�ƃJ�����̍�
    public Action endAction = null;
    int one = 1;
    bool selfOperation = false;//�������Ԃ�

    private void Start()
    {
        screenHeigth = Screen.height / 2;//�X�N���[���̒��_������
        distance = transform.position - player.gameObject.transform.position;
        defPos = transform.position;
        defRot = transform.rotation;
    }

    private void Update()
    {
        if (selfOperation)
        {
            //Player���X�N���[�����W�ɕϊ�
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.gameObject.transform.position);
            Debug.Log(playerScreenPoint.y - screenHeigth);
            //Player�ƒ��S�̍����A���߂��͈͂���ɂ��邩
            if (playerScreenPoint.y - screenHeigth >= range)
            {
                Vector3 move = Vector3.Lerp(transform.position, player.gameObject.transform.position + distance, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, move.y, move.z);
            }
        }
    }

    //���[�h�ύX���Ɏ󂯎��
    public void ChengeSelfOperation(bool ope)
    {
        selfOperation = ope;
    }

    /// <summary>
    /// �J�����̈ʒu���X���C�_�[�œ�����
    /// </summary>
    /// <param name="slider">�������p�̃X���C�_�[</param>
    public void MoveCamera(Slider slider)
    {
        float proportion = 0;
        if (transform.position.z >= criteriaZ)
        {
            proportion = GetCoordinates(slider.value < maxCriteriaPro, slider.value, defPosY);
        }
        else
        {
            proportion = defPosY;
        }

        transform.position = new Vector3(transform.position.x, proportion, defPosZ + slider.value * maxRange);
    }

    /// <summary>
    /// �X���C�_�[�̒l�̊�����ϊ����Ēl��Ԃ�
    /// </summary>
    /// <param name="check">���W�𒴂��Ă��邩</param>
    /// <param name="value">�X���C�_�[�̒l</param>
    /// <param name="def">��̍��W�l</param>
    /// <returns></returns>
    private float GetCoordinates(bool check ,float value, float def)
    {
        if(check)
        {
            return def + (maxHeight * ((value - criteriaPro) * (one / (one - criteriaPro))));
        }
        else
        {
            return def + (maxHeight * ((maxCriteriaPro - criteriaPro) * (one / (one - criteriaPro))));
        }
    }

    public void FinishMove(bool over, bool stop = false)
    {
        Transform nextTransform = null;
        if(over)
        {
            Debug.Log(5665);
            if (stop)
            {
                nextTransform = playerDiePos;
            }
            else
            {
                nextTransform = overPosition;
            }
        }
        else
        {
            nextTransform = clearPosition;
        }

        transform.DOMove(nextTransform.position, 1f);
        transform.DORotateQuaternion(nextTransform.rotation, 1f).OnComplete(() => { endAction(); }) ;
    }

    //�ʒu�����ɖ߂�
    public void CameraMoveAction()
    {
        Debug.Log(defPos);
        transform.DOMove(defPos, 0.5f);
        transform.DORotateQuaternion(defRot, 0.5f).OnComplete(() => { endAction = null; });
    }


}
