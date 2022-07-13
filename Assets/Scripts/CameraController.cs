using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] float defPos;
    [SerializeField] float maxRange;


    public void MoveCamera(Slider slider)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, defPos + slider.value * maxRange);
    }
}
