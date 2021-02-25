using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitName", menuName = "UnitProperties")]
public class UnitProperties : ScriptableObject
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

    [SerializeField]
    public Sprite[] sprites;
}
