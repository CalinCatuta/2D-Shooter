using UnityEngine;

[CreateAssetMenu(fileName = "RangeEnemyScriptableObject", menuName = "ScriptableObjects/RangeEnemy")]
public class RangeEnemyScriptableObject : ScriptableObject {
    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    [SerializeField]
    public float shootRange;
    public float ShootRange { get => shootRange; private set => shootRange = value; }
    [SerializeField]
    public float shootInterval;
    public float ShootInterval { get => shootInterval; private set => shootInterval = value; }
    [SerializeField]
    public float retreatDistance;
    public float RetreatDistance { get => retreatDistance; private set => retreatDistance = value; }
    [SerializeField]
    public float stopDistance;
    public float StopDistance { get => stopDistance; private set => stopDistance = value; }

}