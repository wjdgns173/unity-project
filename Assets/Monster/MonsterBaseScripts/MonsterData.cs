using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Monster/Data")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private float Speed;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float Defense;
    [SerializeField] private float AttackSpeed;

    public float maxHealth    => MaxHealth;
    public float speed        => Speed;
    public float attackDamage => AttackDamage;
    public float defense      => Defense;
    public float attackSpeed  => AttackSpeed;


}
