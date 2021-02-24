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
    public LayerMask restrictedArea;
    public LayerMask walkableArea;

    [HideInInspector]
    public GameObject selectedPawn;

    [HideInInspector]
    public SKStateMachine<BattleSystem> sm;

    [SerializeField]
    public Tilemap grid;

    [HideInInspector]
    public GamePhase gamePhase = GamePhase.PlayerOne;
    
    //[HideInInspector]
    //public int touchedUnitCount = 0;

    [HideInInspector]
    public List<GameObject> touchedPawns;
    
    private void Start()
    {
        sm = new SKStateMachine<BattleSystem>(this, new Idle());
        sm.addState(new MovingPawn());
        sm.addState(new ChangeSide());
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