using UnityEngine;

public class FarmStage4Wilted : FarmBaseState
{
    public override void EnterState(FarmStateManager farm)
    {
        //Debug.Log("Plot counter from wilted: " + FarmStateManager.finishedPlotCounter);
        //Debug.Log("Wilted State");
        farm.stage3.GetComponent<MeshRenderer>().material = farm.wiltedMat;
        farm.PlotCounter();
    }

    public override void UpdateState(FarmStateManager farm)
    {
        
    }
}