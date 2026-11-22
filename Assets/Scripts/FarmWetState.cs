using UnityEngine;

public class FarmWetState : FarmBaseState
{
    float timer = 0;

    public override void EnterState(FarmStateManager farm)
    {
        //Debug.Log("Wet State");
        timer = farm.nextStage;
    }

    public override void UpdateState(FarmStateManager farm)
    {
        // Countdown until next stage
        timer -= Time.deltaTime;

        //Debug.Log("Time til next state switch from watered: " + Mathf.Ceil(timer));

        if (timer <= 0)
        {
            farm.SwitchState(farm.germinationState);
        }
    }
}
