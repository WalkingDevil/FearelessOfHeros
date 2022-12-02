using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create GachaTable")]
public class GachaTable : ScriptableObject
{
    [SerializeField]
    public int probability;  // �m��
    public List<int> character;  // �����X�^�[ID
}
