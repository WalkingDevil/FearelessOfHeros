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
        // �h���b�O�O�̈ʒu���L�����Ă���
        parent = transform.parent;
        defParent = transform.parent;
        this.gameObject.transform.parent = GameObject.Find("SelectionsPanel").transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �h���b�O���͈ʒu���X�V����
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
