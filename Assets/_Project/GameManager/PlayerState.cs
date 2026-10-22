using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected readonly PlayerController p;
    protected PlayerState(PlayerController player) { p = player; }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Tick()
    {

    }
}
