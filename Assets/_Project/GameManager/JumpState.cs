using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    const float TakeoffTime = 0.1f;
    float timer;

    public JumpState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        timer = 0f;
        //  p.VerticalVelocity = Mathf.Sqrt(p.jumpHeight * -2f * p.gravity);
        //  p.TriggerJumpAnim();
    }

    //public override void Tick()
    //{
    //    timer += Time.deltaTime;
    //    p.ApplyGravity();
    //    // p.Move(p.HorizontalVelocity);

    //    if (timer >= TakeoffTime) p.Machine.ChangeState(p.Air);
    //}

    public override void Tick()
    {
        //vaanj toc ban dau la 
        p.VerticalVelocity = Mathf.Sqrt(p.jumpHeight * -2f * p.gravity);
        //bật triiger 
        p.TriggerJumpAnim();

        p.ApplyGravity();
        p.Move();

        //if (timer >= TakeoffTime) p.Machine.ChangeState(p.Air);
        timer += Time.deltaTime;
        if (timer > TakeoffTime)
        {
            //chuyenr sang air state
            StateMachine.Instance.ChangeState(p.Air);
        }
    }
}

