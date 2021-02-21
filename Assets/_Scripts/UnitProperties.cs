using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitName", menuName = "UnitProperties")]
public class UnitProperties : ScriptableObject
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private Tag[] tags;

    [SerializeField]
    private int damageDealt = 0;

    [SerializeField]
    private int moveRange = 0;

    [SerializeField]
    private int attackRange = 0;

    [SerializeField]
    private Sprite[] sprites;
}
