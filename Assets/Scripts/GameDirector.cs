using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] List<UserInterface> towerUser;//タワー用のスクリプト
    [SerializeField] int _towerCount = 0;
    public int towerCount
    {
        get { return _towerCount; }
        set
        {
            if(_towerCount != value)
            {
                _towerCount = value;
                towerUser.RemoveAt(0);
                towerUser[0].SetSlider();
            }
        }
    }

    private void Start()
    {
        towerUser[0].SetSlider();
    }
}
