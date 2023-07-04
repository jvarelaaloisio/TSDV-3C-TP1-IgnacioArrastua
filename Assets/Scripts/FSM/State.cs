using System.Collections.Generic;


public abstract class State
{
    public string name;
    protected StateMachine stateMachine;

    protected State(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    public virtual void OnEnter(){}
    public virtual void OnUpdate(){}
    public virtual void OnFixedUpdate(){}
    public virtual void OnExit(){}
    
}