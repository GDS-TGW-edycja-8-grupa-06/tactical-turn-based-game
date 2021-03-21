using UnityEngine;
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
        Unit immobilizedUnit;

        if (colliderLayerMask != opponentLayerMask)
        {
            return;
        }

        immobilizedUnit = collider.GetComponent<Unit>();
        immobilizedUnit.sm.changeState<DecreaseActionCount>();

        Debug.LogFormat("{0} entered immobilzed area...", collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LayerMask opponentLayerMask = unit.side == Bodzio2k.Side.PlayerOne ? LayerMask.NameToLayer("PlayerTwo") : LayerMask.NameToLayer("PlayerOne");
        GameObject collider = collision.gameObject;
        LayerMask colliderLayerMask = collider.layer;
        Unit immobilizedUnit;

        if (colliderLayerMask != opponentLayerMask)
        {
            return;
        }

        immobilizedUnit = collider.GetComponent<Unit>();
        immobilizedUnit.actionsRemaining = 2;

        Debug.LogFormat("{0} leaved immobilzed area...", collision.gameObject.name);
    }
}
