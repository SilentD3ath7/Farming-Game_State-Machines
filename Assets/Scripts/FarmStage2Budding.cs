using UnityEngine;

public class FarmStage2Budding : FarmBaseState
{
    float timer = 0;

    public override void EnterState(FarmStateManager farm)
    {
        //Debug.Log("Budding State");
        timer = farm.nextStage;
        farm.stage2.gameObject.SetActive(true);
        farm.stage1.gameObject.SetActive(false);
    }

    public override void UpdateState(FarmStateManager farm)
    {
        // Countdown until next stage
        timer -= Time.deltaTime;

        //Debug.Log("Time til next state switch from budding: " + Mathf.Ceil(timer));

        if (timer <= 0)
        {
            farm.SwitchState(farm.matureState);
        }
    }
}
