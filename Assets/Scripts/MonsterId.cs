using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterId
{
    private int id;
    private int hp;
    private int attack;
    private int difence;
    private int cost;

    /// <summary>
    /// �����X�^�[ID
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="hp">HP</param>
    /// <param name="attack">�U����</param>
    /// <param name="difence">�h���</param>
    /// <param name="cost">�R�X�g</param>
   public MonsterId(int id, int hp, int attack, int difence, int cost)
    {
        this.id = id;
        this.hp = hp;
        this.attack = attack;
        this.difence = difence;
        this.cost = cost;
    }
}
