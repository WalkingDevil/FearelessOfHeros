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
            if(slider.value < maxCriteriaPro)
            {
                proportion = defPosY + (maxHeight * ((slider.value - criteriaPro) * (one / (one - criteriaPro))));
            }
            else
            {
                proportion = defPosY + (maxHeight * ((maxCriteriaPro - criteriaPro) * (one / (one - criteriaPro))));
            }
        }
        else
        {
            proportion = defPosY;
        }

        transform.position = new Vector3(transform.position.x, proportion, defPosZ + slider.value * maxRange);
    }
}
