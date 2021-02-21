using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.Tilemaps;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerOneLayerMask;

    [HideInInspector]
    public GameObject selectedUnit;

    [HideInInspector]
    public SKStateMachine<BattleSystem> sm;

    [SerializeField]
    public Tilemap[] walkableArea;

    private void Start()
    {
        sm = new SKStateMachine<BattleSystem>(this, new Idle());
        sm.addState(new MovingPawn());
    }

    void Update()
    {
        sm.update(Time.deltaTime);        
    }
}
