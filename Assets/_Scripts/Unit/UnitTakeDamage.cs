using Prime31.StateKit;

internal class UnitTakeDamage : SKState<Unit>
{
    public override void begin()
    {
        base.begin();

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