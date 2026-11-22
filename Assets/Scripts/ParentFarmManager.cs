using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ParentFarmManager : MonoBehaviour, IObserver, IPublisher
{
    // Just to have 1 variable that the developer can easily change
    //[SerializeField] protected int timeToNextStage = 3;
    //public static int nextStage = 0;
    //protected int plotSize = 9;
    int totalFarmPlots = 3;
    static int totalFarmPlotCounter = 2;
    [HideInInspector] public ParentFarmMediator parentFarmMediator = null;
    //[SerializeField] IPublisher publisher;
    List<IObserver> subscribers = new List<IObserver>();
    public delegate void NotifyDelegate(PublisherConditions condition);
    public event NotifyDelegate notifyEventManger;

    // Start is called before the first frame update
    void Start()
    {
        //farmStateManager.Subscribe(this);
        // Adds a subscriber (observer) to the publisher (parentFarmMediator)
        if (this.parentFarmMediator != null) {this.parentFarmMediator.notifyParentFarm += this.Notify;}
    }

    public void Notify(PublisherConditions condition)
    {
        if (condition == PublisherConditions.plotDone)
        {
            totalFarmPlotCounter++;
            Debug.Log("Farm plots that are done: " + totalFarmPlotCounter);
        }

        if (totalFarmPlotCounter == totalFarmPlots)
        {
            Publish(PublisherConditions.winGame);
        }
    }

    public void Publish(PublisherConditions condition)
    {
        //if (this.notifyEventManger != null) 
        {
            Debug.Log("condition sent to eventmanager from parentFarmManager " + condition);
            notifyEventManger(condition);
        }
    }

    public void Subscribe(IObserver observer)
    {
        subscribers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        subscribers.Remove(observer);
    }
}
