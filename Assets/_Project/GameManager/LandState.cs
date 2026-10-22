using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LandState : PlayerState
{
    public float landDuration = 0.3f;   // chinh khop do dai clip Landing
    float timer;

    public LandState(PlayerController player) : base(player) { }

    public override void Enter() { timer = 0f; }

    public override void Tick()
    {
        timer += Time.deltaTime;

        // Giam toc nhanh khi tiep dat
        p.HorizontalVelocity = Vector3.Lerp(p.HorizontalVelocity, Vector3.zero, 10f * Time.deltaTime);
        p.ApplyGravity();
        p.Move();

        // if (!p.IsGrounded) { StateMachine.Instance.ChangeState(p.Air); return; }
        if (timer >= landDuration) StateMachine.Instance.ChangeState(p.Locomotion);
    }

}
