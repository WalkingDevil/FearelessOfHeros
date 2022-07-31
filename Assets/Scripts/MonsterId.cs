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
    /// モンスターID
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="hp">HP</param>
    /// <param name="attack">攻撃力</param>
    /// <param name="difence">防御力</param>
    /// <param name="cost">コスト</param>
   public MonsterId(int id, int hp, int attack, int difence, int cost)
    {
        this.id = id;
        this.hp = hp;
        this.attack = attack;
        this.difence = difence;
        this.cost = cost;
    }
}
