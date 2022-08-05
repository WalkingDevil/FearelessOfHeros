using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Transform parent;
    private Transform defParent;
    private string hitField = "SelectField";
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        parent = transform.parent;
        defParent = transform.parent;
        this.gameObject.transform.parent = GameObject.Find("SelectionsPanel").transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中は位置を更新する
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.gameObject.transform.parent = parent.transform;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag(hitField))
            {
                if (hit.gameObject.transform.childCount < 4)
                {
                    parent = hit.gameObject.transform;
                }
            }
        }
    }
}
