using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController player;

    [SerializeField] float defPosY;//標準のY座標
    [SerializeField] float defPosZ;//標準のZ座標
    [SerializeField] float criteriaPro;//基準の割合
    [SerializeField] float maxCriteriaPro;//最大基準の割合
    [SerializeField] float maxRange;//最大の奥行
    [SerializeField] float maxHeight;//最大のカメラの高さ
    [SerializeField] float criteriaZ;//基準のZ座標

    [SerializeField] Transform overPosition;
    [SerializeField] Transform clearPosition;
    [SerializeField] float range = 20f;//差の範囲
    [SerializeField] float moveSpeed = 0.1f;//動かすスピード
    private float screenHeigth;//スクリーンの高さ
    private Vector3 distance;//Playerとカメラの差
    public Action endAction = null;
    int one = 1;
    bool selfOperation = false;//自操作状態か

    private void Start()
    {
        screenHeigth = Screen.height / 2;//スクリーンの中点を入れる
        distance = transform.position - player.gameObject.transform.position;
    }

    private void Update()
    {
        if (selfOperation)
        {
            //Playerをスクリーン座標に変換
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.gameObject.transform.position);
            Debug.Log(playerScreenPoint.y - screenHeigth);
            //Playerと中心の差を絶対値で返し、決めた範囲以内であるか
         //   if (Mathf.Abs(playerScreenPoint.y - screenHeigth) <= range)
             if (playerScreenPoint.y - screenHeigth >= range)
                {
              Vector3 move = Vector3.Lerp(transform.position, player.gameObject.transform.position + distance, moveSpeed * Time.deltaTime);
              transform.position = new Vector3(transform.position.x, move.y, move.z);
            }
        }
    }

    //モード変更時に受け取る
    public void ChengeSelfOperation(bool ope)
    {
        selfOperation = ope;
    }

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
            if(selfOperation)
            {
                Transform opePos = null;
                opePos.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 15f);
                opePos.rotation = overPosition.rotation;
                nextTransform = opePos;
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

    public void CameraMoveAction(Transform transform)
    {
        transform.DOMove(transform.position, 1f);
        transform.DORotateQuaternion(transform.rotation, 1f).OnComplete(() => { endAction(); });
    }


}
