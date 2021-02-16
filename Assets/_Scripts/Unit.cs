using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitName", menuName = "Unit")]
public class Unit : ScriptableObject
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private Tag[] tags;

    [SerializeField]
    private int damageDealt = 0;

    [SerializeField]
    private Sprite[] sprites;
}
