using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostLineGenerator : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] Image linePrefab;
    [SerializeField] RectTransform rect;
    [SerializeField] float pos;
    [SerializeField] float diffY;
    [SerializeField] float diffX;
    private List<float> linePos;
    private int cost;

    void Start()
    {
        cost = (int)gameDirector.cost;
        linePos = new List<float>(cost);
        PositionSetting();
        
    }

    private void PositionSetting()
    {
        for(int s = 0; s < cost - 1; s++)
        {
            var num = pos - (diffX * (1 + s));
            Debug.Log(num);
            linePos.Add(num);
        }

        PullLine();
    }

    private void PullLine()
    {
        for (int l = 0; l < linePos.Count; l++)
        {
            Instantiate(linePrefab , new Vector2(linePos[l], this.gameObject.transform.position.y), Quaternion.identity, rect.transform);
        }

    }
}
