using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject overlayTilePrefab;

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
        
        if (overlay != null)
        {
            return;
        }

        Vector3 position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

        GameObject overlayTile = Instantiate(overlayTilePrefab, position, Quaternion.identity, overlay.transform);
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