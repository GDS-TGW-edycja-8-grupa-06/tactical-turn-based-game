using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.Tilemaps;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    public LayerMask playerOneLayerMask;
    public LayerMask playerTwoLayerMask;

    [HideInInspector]
    public GameObject selectedUnit;

    [HideInInspector]
    public SKStateMachine<BattleSystem> sm;

    [SerializeField]
    public Tilemap[] walkableArea;

    [HideInInspector]
    public GamePhase gamePhase = GamePhase.PlayerOne;

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

public enum GamePhase
{
    PlayerOne,
    PlayerTwo
}