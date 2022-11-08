using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManeger
{
    public StatusManeger(int maxhp, int attack, float defence, int cost = 0)
    {
        this.maxHp = maxhp;
        this.attack = attack;
        this.defence = defence;
        this.cost = cost;
    }


    private int _maxHp;
    public int maxHp
    {
        get { return _maxHp; }
        set
        {
            if (_maxHp != value)
            {
                _maxHp = value;
                hp = _maxHp;
            }
        }
    }

    private float _hp;
    public float hp
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

    private float _defence;
    public float defence
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

    private int _cost;
    public int cost
    {
        get { return _cost; }
        set
        {
            if(_cost != value)
            {
                _cost = value;
            }
        }
    }

    public float DamageCalculation(float damege)
    {
        var num = damege / defence;

        if(num != 0)
        {
            return hp -= num;
        }
        else
        {
            return hp -= 0.1f;
        }
    }

    public float PlusHp(int heel)
    {
        return hp += heel;
    }

    public float GetRatio()
    {
        return hp / maxHp;
    }
}
