using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    public IState currentState;
    public void ChangeState(IState newState)
    {
        if (newState == null || newState == currentState) return;
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void Tick() => currentState?.Tick();
}
