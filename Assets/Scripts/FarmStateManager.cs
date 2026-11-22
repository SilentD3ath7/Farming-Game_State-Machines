using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmStateManager : MonoBehaviour
{
    FarmBaseState currentState;

    public FarmDryState dryState = new FarmDryState();
    public FarmWetState wetState = new FarmWetState();
    public FarmStage1Germination germinationState = new FarmStage1Germination();
    public FarmStage2Budding budState = new FarmStage2Budding();
    public FarmStage3Mature matureState = new FarmStage3Mature();
    public FarmStage4Wilted wiltedState = new FarmStage4Wilted();
    public IParentFarmMediator parentFarmMediator = null;

    public int timeToNextStage = 3;
    public static int finishedPlotCounter = 0;
    [HideInInspector] public int nextStage = 0;
    int plotSize = 9;

    public GameObject stage1, stage2, stage3;

    public Material wiltedMat;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Plot counter from base start: " + finishedPlotCounter);
        //Debug.Log("timeToNextStage: " + timeToNextStage);
        nextStage = timeToNextStage;

        // Starting state for the machine
        currentState = dryState;

        // "this" is a reference to the current script
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(FarmBaseState state)
    {
        // Resets the timer for the state
        nextStage = timeToNextStage;

        currentState = state;
        state.EnterState(this);

        //Debug.Log("Plot counter from base switch state: " + finishedPlotCounter);
    }

    public void PlotCounter()
    {
        finishedPlotCounter++;
        //Debug.Log("Plots finished: " + finishedPlotCounter);

        if (finishedPlotCounter % plotSize == 0)
        {
            this.parentFarmMediator.FarmPlotCounter();
            //Debug.Log("Farm plot done!");
        }
    }


}
