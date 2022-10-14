using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create GachaTable")]
public class GachaTable : ScriptableObject
{
    [SerializeField]
    public string name;
    public int probability;
    public List<string> character;
}
