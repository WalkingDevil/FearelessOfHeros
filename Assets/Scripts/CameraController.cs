using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] float defPosY;//�W����Y���W
    [SerializeField] float defPosZ;//�W����Z���W
    [SerializeField] float criteriaPro;//��̊���
    [SerializeField] float maxCriteriaPro;//�ő��̊���
    [SerializeField] float maxRange;//�ő�̉��s
    [SerializeField] float maxHeight;//�ő�̃J�����̍���
    [SerializeField] float criteriaZ;//���Z���W

    int one = 1;

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
}
