using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    public UnitProperties unitProperties;

    [SerializeField]
    public Side side;

    [SerializeField]
    public GameObject contextMenu;

    private GameObject contextMenuInstance;

    public void ShowContextMenu()
    {
        if (contextMenu != null)
        {
            contextMenuInstance = Instantiate(contextMenu, transform.position, Quaternion.identity, transform);
        }
    }

    public void HideContextMenu()
    {
        if (contextMenuInstance != null)
        {
            Destroy(contextMenuInstance);
        }
    }
}

public enum Side
{
    PlayerOne,
    PlayerTwo
}