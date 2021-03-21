﻿using UnityEngine;
using Bodzio2k.Unit;
using Bodzio2k.BattleSystem;

public class Immobilzer : MonoBehaviour
{
    Unit unit = null;

    private void Start()
    {
        unit = transform.parent.gameObject.GetComponent<Unit>();

        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask opponentLayerMask = unit.side == Bodzio2k.Side.PlayerOne ? LayerMask.NameToLayer("PlayerTwo") : LayerMask.NameToLayer("PlayerOne");
        GameObject collider = collision.gameObject;
        LayerMask colliderLayerMask = collider.layer;

        if (colliderLayerMask != opponentLayerMask)
        {
            return;
        }

        Debug.LogFormat("{0} entered immobilzed area...", collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LayerMask opponentLayerMask = unit.side == Bodzio2k.Side.PlayerOne ? LayerMask.NameToLayer("PlayerTwo") : LayerMask.NameToLayer("PlayerOne");
        GameObject collider = collision.gameObject;
        LayerMask colliderLayerMask = collider.layer;

        if (colliderLayerMask != opponentLayerMask)
        {
            return;
        }


        Debug.LogFormat("{0} leaved immobilzed area...", collision.gameObject.name);
    }
}
