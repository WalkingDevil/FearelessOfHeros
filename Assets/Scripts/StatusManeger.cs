﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManeger
{
    public StatusManeger(int maxhp, int attack, int defence)
    {
        this.maxHp = maxhp;
        this.attack = attack;
        this.defence = defence;
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

    private int _attack;
    public int attack
    {
        get { return _attack; }
        set
        {
            if(_attack != value)
            {
                _attack = value;
            }
        }
    }

    private int _defence;
    public int defence
    {
        get { return _defence; }
        set
        {
            if (_defence != value)
            {
                _defence = value;
            }
        }
    }

    public int DamageCalculation(int damege)
    {
        return hp -= damege / defence;
    }

    public int PlusHp(int heel)
    {
        return hp += heel;
    }
}
