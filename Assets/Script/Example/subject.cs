using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subject : MonoBehaviour
{
private List<IOpserver> observers = new List<IOpserver>();

    public void AddObserveer(IOpserver observer) => observers.Add(observer);
    public void ReemoveObserver(IOpserver observer) => observers.Remove(observer);

    private void Start()
    {
        Notify();
    }

    private void Notify()
    {
        foreach (IOpserver observer in observers)
        {
            observer.OnNotify();
        }
    }
}
