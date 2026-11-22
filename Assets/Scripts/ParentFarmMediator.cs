using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFarmMediator : MonoBehaviour, IParentFarmMediator, IPublisher
{
    public List<FarmStateManager> farmStateManagers = new();
    List<IObserver> subscribers = new List<IObserver>();
    public delegate void NotifyDelegate(PublisherConditions condition);
    public event NotifyDelegate notifyParentFarm;

    // Start is called before the first frame update
    void Start()
    {
        foreach (FarmStateManager stateManager in this.farmStateManagers)
        {
            stateManager.parentFarmMediator = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FarmPlotCounter()
    {
        Publish(PublisherConditions.plotDone);
    }

    public void Publish(PublisherConditions condition)
    {
        notifyParentFarm(condition);
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
