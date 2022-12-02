using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create GachaTable")]
public class GachaTable : ScriptableObject
{
    [SerializeField]
    public int probability;  // 確率
    public List<int> character;  // モンスターID
}
