using Prime31.StateKit;

internal class UnitChangeDamageMultiplier : SKState<Unit>
{
    public override void begin()
    {
        base.begin();

        float damageMultiplier = 0.0f;

        damageMultiplier = _context.unitProperties.damageMultiplier == 1.0f ? 0.5f : 1.0f;

        _context.unitProperties.damageMultiplier = damageMultiplier;

        _context.sm.changeState<UnitIdle>();
    }

    public override void end()
    {
        base.end();
    }

    public override void update(float deltaTime)
    {
        return;
    }
}