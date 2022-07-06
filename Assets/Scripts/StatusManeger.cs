using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManeger
{
    public StatusManeger(int maxhp)
    {
        this.maxHp = maxhp;
    }

    private int _maxHp;
    public int maxHp
    {
        set
        {
            if (_maxHp != value)
            {
                _maxHp = value;
                hp = _maxHp;
            }
        }
    }

    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (_hp != value)
            {
                _hp = value;
            }
        }
    }

    public int MinusHp(int damege)
    {
        return hp -= damege;
    }

    public int PlusHp(int heel)
    {
        return hp += heel;
    }
}
