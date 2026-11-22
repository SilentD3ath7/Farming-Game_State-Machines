using UnityEngine;

public class FarmDryState : FarmBaseState
{
    Material mat;
    
    public override void EnterState(FarmStateManager farm)
    {
        //Debug.Log("Dry State");
    }

    public override void UpdateState(FarmStateManager farm)
    {
        mat = farm.gameObject.GetComponent<MeshRenderer>().material;
        //Debug.Log("Waiting for water, current MAT: " + mat.name);

        if (mat.name == "WateredField_MAT (Instance)")
        {
            farm.SwitchState(farm.wetState);
            //Debug.Log("I am watered");
        }
    }
}
