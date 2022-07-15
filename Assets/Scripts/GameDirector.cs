using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{

    [SerializeField] int _towerCount = 0;
    public int towerCount
    {
        get { return _towerCount; }
        set
        {
            if(_towerCount != value)
            {
                _towerCount = value;
            }
        }
    }
}
