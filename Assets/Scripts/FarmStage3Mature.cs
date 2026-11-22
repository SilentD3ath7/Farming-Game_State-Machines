using UnityEngine;

public class FarmStage3Mature : FarmBaseState
{
    float timer = 0;

    public override void EnterState(FarmStateManager farm)
    {
        //Debug.Log("Mature State");
        timer = farm.nextStage;
        farm.stage3.gameObject.SetActive(true);
        farm.stage2.gameObject.SetActive(false);
    }

    public override void UpdateState(FarmStateManager farm)
    {
        // Countdown until next stage
        timer -= Time.deltaTime;

        //Debug.Log("Time til next state switch from mature: " + Mathf.Ceil(timer));
        
        if (timer <= 0)
        {
            farm.SwitchState(farm.wiltedState);
        }
    }
}
