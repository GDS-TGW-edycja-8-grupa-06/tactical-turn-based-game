using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using System.Linq;

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
        sm.addState(new ShowingRangeOverlay());
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

    public void CreateRangeOverlay(OverlayType overlayType)
    {
        int range = overlayType == OverlayType.Move ? unitProperties.moveRange : unitProperties.attackRange;
        GameObject overlay = transform.Find("Overlay").gameObject;

        if (overlay == null)
        {
            return;
        }

        if (overlay.transform.childCount != 0)
        {
            overlay.SetActive(true);
        }

        List<Vector3> overlayPositions = GetOverlayPositions(range);

        foreach (Vector3 position in overlayPositions)
        {
            Instantiate(overlayTilePrefab, overlay.transform.position + position, Quaternion.identity, overlay.transform);
        }

        return;
    }

    public void HideRangeOverlay()
    {
        GameObject overlay = transform.Find("Overlay").gameObject;

        overlay.SetActive(false);
    }

    private List<Vector3> GetOverlayPositions(int range)
    {
        List<Vector3> positions = new List<Vector3>();
        IEnumerable<int> ranges = Enumerable.Range(-range, range * 2 + 1).Where(r => r == -range || r == range);

        foreach (int h in ranges)
        {
            positions.Add(new Vector3(h, 0, transform.position.z));
        }

        foreach (int v in ranges)
        {
            positions.Add(new Vector3(0, v, transform.position.z));
        }

        return positions;
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