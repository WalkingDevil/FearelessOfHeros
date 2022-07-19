using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManeger
{
    public StatusManeger(int maxhp, int attack, int defence, int cost = 0, int maxExp = 0)
    {
        this.maxHp = maxhp;
        this.attack = attack;
        this.defence = defence;
        this.cost = cost;
        this.maxExp = maxExp;
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

    private int _maxExp;  //最大経験値
    public int maxExp
    {
        get
        {
            return _maxExp;
        }
        private set
        {
            if (_maxExp != value)
            {
                _maxExp = value;
            }
        }
    }
    private int _exp;  //経験値

    public int exp
    {
        get
        {
            return _exp;
        }
        private set
        {
            if (_exp != value)
            {
                _exp = value;
                if (_maxExp <= _exp)
                {
                    _exp = _exp - _maxExp;
                }
            }
        }
    }

    public int DamageCalculation(int damege)
    {
        var num = damege / defence;

        if(num != 0)
        {
            return hp -= num;
        }
        else
        {
            return hp -= 1;
        }
    }

    public int PlusHp(int heel)
    {
        return hp += heel;
    }

    /// <summary>
    /// 経験値を取得した時の処理
    /// </summary>
    /// <param name="exp">取得した経験値</param>
    public void UpExp(int exp)
    {
        this.exp += exp;
    }
    /// <summary>
    /// レベルアップするために必要な経験値の上限を変える
    /// </summary>
    /// <param name="maxExp">レベルアップに必要な経験値</param>
    public void MaxExp(int maxExp)
    {
        this.maxExp += maxExp;
    }
}
