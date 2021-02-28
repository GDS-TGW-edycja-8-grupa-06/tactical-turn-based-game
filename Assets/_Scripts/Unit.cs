using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class Unit : MonoBehaviour
{
    [SerializeField]
    public UnitProperties unitProperties;

    [SerializeField]
    public UnitSide side;

    [SerializeField]
    public GameObject contextMenu;
    private GameObject contextMenuInstance;

    [HideInInspector]
    public UnitAction unitAction;

    [SerializeField]
    public GameObject overlayTilePrefab;

    [HideInInspector]
    public SKStateMachine<Unit> sm;

    [HideInInspector]
    public BattleSystem battleSystem;

    private void Start()
    {
        battleSystem = GameObject.Find("/BattleSystem").GetComponent<BattleSystem>();

        sm = new SKStateMachine<Unit>(this, new UnitIdle());

        sm.addState(new ShowingContextMenu());
    }
    public void ShowContextMenu()
    {
        if (contextMenu != null)
        {
            Vector3 position = GetContextMenuPostion();

            contextMenuInstance = Instantiate(contextMenu, position, Quaternion.identity);
        }
    }

    public void HideContextMenu()
    {
        if (contextMenuInstance != null)
        {
            Destroy(contextMenuInstance);
        }
    }

    private Vector3 GetContextMenuPostion()
    {
        Vector3 position = transform.position;

        position.y = position.y == -7.0f ? -6.0f : position.y;

        return transform.position;
    }
}

public enum UnitSide
{
    PlayerOne,
    PlayerTwo
}

public enum OverlayType
{
    Move,
    Attack
}