using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPublisher
{
    /* private List<IObserver> observers = new List<IObserver>();

    public void Subscribe(IObserver subscriber)
    {
        observers.Add(subscriber);
    }

    public void Unsubscribe(IObserver subscriber)
    {
        observers.Remove(subscriber);
    }

    protected void Publish(PublisherConditions condition)
    {
        observers.ForEach((observer) => {observer.OnNotify(condition);});
    } */

    public void Publish(PublisherConditions condition);
    public void Subscribe(IObserver observer);
    public void Unsubscribe(IObserver observer);

}
