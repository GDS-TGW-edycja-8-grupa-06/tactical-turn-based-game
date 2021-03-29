using UnityEngine;
using Bodzio2k.Unit;

[CreateAssetMenu(fileName = "UnitName", menuName = "Unit Properties")]
public class Properties : ScriptableObject
{
    [SerializeField]
    public int health = 100;

    [SerializeField]
    public Tag[] tags;

    [SerializeField]
    public int damageDealt = 0;

    [SerializeField]
    public int moveRange = 0;

    [SerializeField]
    public int attackRange = 0;

    [HideInInspector]
    public float damageMultiplier = 1.0f;
}
