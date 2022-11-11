using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManeger
{
    public StatusManeger(int maxhp, int attack, float defence, int cost, int attributes)
    {
        this.maxHp = maxhp;
        this.attack = attack;
        this.defence = defence;
        this.cost = cost;
        this.attributes = attributes;
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


    private int attributes;

    public float DamageCalculation(float damege, int attributes)
    {
        int attack = (int)(damege * DamegeRate(attributes));

        if(attack != 0)
        {
            return hp -= attack / defence;
        }
        else
        {
            return hp = 0f;
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

    /// <summary>
    /// ダメージの属性倍率
    /// </summary>
    /// <param name="attributes">敵の属性</param>
    /// <returns></returns>
    private float DamegeRate(int attributes)
    {
        switch(this.attributes == 3)
        {
            case true:
                switch (attributes == 3)
                {
                    case true:
                        return 1f;
                    case false:
                        return 0.5f;
                }    
            case false:
                switch(attributes == 3)
                {
                    case true:
                        return 1.5f;
                    case false:
                        switch((this.attributes - attributes + 3) % 3)
                        {
                            case 0:
                                return 0.75f;
                            case 1:
                                return 0;
                            case 2:
                                return 1;
                        }
                        break;
                }
                break;
        }
        return 1;
    }


}
