using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonPersistent<InputManager>, IObservable
{
    private List<IObserver> observers;
    private Vector2 moveInput;
    private float left;
    private float right;

    public Vector2 MoveInput{get => new Vector2(left, right);}

    protected override void Awake() {
        base.Awake();
        observers = new List<IObserver>();
    }

    public void OnLeft(InputValue ctx){
        left = ctx.Get<float>();
        NotifyObservers();
    }
    public void OnRight(InputValue ctx){
        right = ctx.Get<float>();
        NotifyObservers();
    }

    public void RegisterObserver(IObserver o) => observers.Add(o);
    public void RemoveObserver(IObserver o) => observers.Add(o);
    public void NotifyObservers(){
        foreach(IObserver o in observers){
            o.UpdateObserver();
        }
    }
}
