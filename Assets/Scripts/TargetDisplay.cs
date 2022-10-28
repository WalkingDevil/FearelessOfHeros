using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDisplay : MonoBehaviour
{
    private Transform target;
    [SerializeField] RectTransform tageImage;
    void Update()
    {
        if(target != null)
        {
            Vector3 targetScreenPoint = Camera.main.WorldToScreenPoint(target.gameObject.transform.position);
            tageImage.position = targetScreenPoint;
        }
    }

    public void GetTarget(Transform tage)
    {
        target = tage;
    }
}
