using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] float defPosY;//標準のY座標
    [SerializeField] float defPosZ;//標準のZ座標
    [SerializeField] float criteriaPro;//基準の割合
    [SerializeField] float maxCriteriaPro;//最大基準の割合
    [SerializeField] float maxRange;//最大の奥行
    [SerializeField] float maxHeight;//最大のカメラの高さ
    [SerializeField] float criteriaZ;//基準のZ座標

    [SerializeField] Transform overPosition;
    [SerializeField] Transform clearPosition;
    public Action endAction = null;
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
            proportion = GetCoordinates(slider.value < maxCriteriaPro, slider.value, defPosY);
        }
        else
        {
            proportion = defPosY;
        }

        transform.position = new Vector3(transform.position.x, proportion, defPosZ + slider.value * maxRange);
    }

    /// <summary>
    /// スライダーの値の割合を変換して値を返す
    /// </summary>
    /// <param name="check">座標を超えているか</param>
    /// <param name="value">スライダーの値</param>
    /// <param name="def">基準の座標値</param>
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

    public void FinishMove(bool over)
    {
        Transform nextTransform = null;
        if(over)
        {
            nextTransform = overPosition;
        }
        else
        {
            nextTransform = clearPosition;
        }

        transform.DOMove(nextTransform.position, 1f);
        transform.DORotateQuaternion(nextTransform.rotation, 1f).OnComplete(() => { endAction(); }) ;
    }


}
