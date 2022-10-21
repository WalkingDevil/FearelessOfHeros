using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create GachaTable")]
public class GachaTable : ScriptableObject
{
    [SerializeField]
    public int name;
    public int probability;
    public List<int> character;
}
