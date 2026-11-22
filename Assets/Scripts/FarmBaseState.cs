using UnityEngine;

public abstract class FarmBaseState
{
    public abstract void EnterState(FarmStateManager farm);

    public abstract void UpdateState(FarmStateManager farm);
}
