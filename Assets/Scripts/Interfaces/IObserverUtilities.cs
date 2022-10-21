using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable{
    public void RegisterObserver(IObserver o);
    public void RemoveObserver(IObserver o);
    public void NotifyObservers();
}

public interface IObserver{
    public void UpdateObserver();
}
