using UnityEngine;
using System;

[Serializable]
public class UnitStats
{
    public float damage;
    public float health;
    public float attackRange;
    public float fireRate;
    public int teamAffiliation; //int so that it's lower in byte size
}
