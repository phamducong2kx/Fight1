using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionState : PlayerState
{
    public LocomotionState(PlayerController player) : base(player) { }

    public override void Tick()
    {
        Debug.Log("chay locomotion");
        p.RotateToCamera();

        if (!p.IsGrounded) { StateMachine.Instance.ChangeState(p.Air); return; }
        if (p.JumpPressed) { StateMachine.Instance.ChangeState(p.Jump); return; }
        //
        bool sprint = p.SprintHeld && p.MoveInput.sqrMagnitude > 0.01f;

        // Blend tree: di = 1, chay = 2
        p.SetLocomotionAnimation(p.MoveInput * (sprint ? 2f : 1f));

        float speed = sprint ? p.runSpeed : p.walkSpeed;
        p.HorizontalVelocity = p.GetMoveDirection() * speed;

        p.ApplyGravity();
        p.Move();
    }
}
