using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public enum StructureType { UnitGen, ResourceGen, Barricade, Tower, Other };

[CreateAssetMenu(fileName = "StructureStats", menuName = "Scriptable Objects/StructureStats")]
public class StructureStats : ScriptableObject
{
    public float damage;
    public float unitDamage;

    public float health;
    public float unitHealth;

    public float attackRange;
    public float unitAttackRange;

    public float fireRate;
    public float unitFireRate;

    public float spawnRate;
    public int teamAffiliation;
    private StructureType structType;
}
