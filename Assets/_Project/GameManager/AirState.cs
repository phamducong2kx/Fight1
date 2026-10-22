using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirState : PlayerState
{
    public AirState(PlayerController player) : base(player)
    {
    }
    public override void Tick()
    {
        // p.RotateToCamera();
        // p.ApplyGravity();

        //nếu chạm đất chuyển sang land state
        if (p.IsGrounded && p.VerticalVelocity < 0)
        {
            StateMachine.Instance.ChangeState(p.Land);
        }
    }
}
