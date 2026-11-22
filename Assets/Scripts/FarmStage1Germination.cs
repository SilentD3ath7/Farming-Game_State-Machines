using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FarmStage1Germination : FarmBaseState
{
    float timer = 0;

    public override void EnterState(FarmStateManager farm)
    {
        //Debug.Log("Germination State");
        timer = farm.nextStage;
        farm.stage1.gameObject.SetActive(true);
    }

    public override void UpdateState(FarmStateManager farm)
    {
        // Countdown until next stage
        timer -= Time.deltaTime;

        //Debug.Log("Time til next state switch from germination: " + Mathf.Ceil(timer));

        if (timer <= 0)
        {
            farm.SwitchState(farm.budState);
        }
    }
}
