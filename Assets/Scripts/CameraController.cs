using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] float defPosY;//標準のY座標
    [SerializeField] float defPosZ;//標準のZ座標
    [SerializeField] float criteriaPro;//基準の割合
    [SerializeField] float maxCriteriaPro;//最大基準の割合
    [SerializeField] float maxRange;//最大の奥行
    [SerializeField] float maxHeight;//最大のカメラの高さ
    [SerializeField] float criteriaZ;//基準のZ座標

    int one = 1;

    /// <summary>
    /// カメラの位置をスライダーで動かす
    /// </summary>
    /// <param name="slider">動かす用のスライダー</param>
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
