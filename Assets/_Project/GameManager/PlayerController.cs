using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float walkSpeed = 2f;
    public float runSpeed = 4.5f;
    public float rotateSpeed = 12f;
    public float animDamp = 0.1f;      // do muot khi blend animation
    public float airControl = 3f;

    [Header("Jump / Gravity")]
    public float jumpHeight = 1.2f;
    public float gravity = -20f;

    // Animator hashes (nhanh hon dung string)
    static readonly int MoveXHash = Animator.StringToHash("MoveX");
    static readonly int MoveYHash = Animator.StringToHash("MoveY");
    static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    static readonly int VerticalVelHash = Animator.StringToHash("VerticalVelocity");
    static readonly int JumpHash = Animator.StringToHash("Jump");

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    //   public StateMachine Machine { get; private set; }

    // Input
    public Vector2 MoveInput { get; private set; }
    public bool SprintHeld { get; private set; }
    public bool JumpPressed { get; private set; }

    // Physics state
    public bool IsGrounded { get; private set; }
    public float VerticalVelocity;
    public Vector3 HorizontalVelocity;

    // States
    public IState Locomotion { get; private set; }
    public IState Jump { get; private set; }
    public IState Air { get; private set; }
    public IState Land { get; private set; }

    Transform cam;

    void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        cam = Camera.main != null ? Camera.main.transform : null;
        Debug.Log("gia tri cua camera la " + cam);

        Locomotion = new LocomotionState(this);
        Jump = new JumpState(this);
        VerticalVelocity = -2f;
        Air = new AirState(this);
        Land = new LandState(this);
    }

    void Start()
    {
        StateMachine.Instance.ChangeState(Locomotion);
    }


    void Update()
    {
        ReadInput();
        IsGrounded = Controller.isGrounded;     // ket qua cua lan Move truoc
        Animator.SetBool(IsGroundedHash, IsGrounded);
        StateMachine.Instance.Tick();
    }

    // đọc input vào
    void ReadInput()
    {
        MoveInput = Vector2.ClampMagnitude(
           new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);

        // float x = Input.GetAxisRaw("Horizontal");
        //  float y = Input.GetAxisRaw("Vertical");
        //  MoveInput = new Vector2(x, y);
        //  MoveInput.Normalize();
        Debug.Log("gia tri cua x va y saiu khi normalize la " + MoveInput.x + ", " + MoveInput.y);
        //giữ phím
        SprintHeld = Input.GetKey(KeyCode.LeftShift);
        //ấn xuống
        JumpPressed = Input.GetButtonDown("Jump");
    }


    // ---------- Helpers cho cac State goi ----------

    public void RotateToCamera()
    {
        if (cam == null) return;
        var target = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, rotateSpeed * Time.deltaTime);
    }

    // Huong di theo huong nhan vat dang quay (strafe style)
    public Vector3 GetMoveDirection()
    {
        return transform.right * MoveInput.x + transform.forward * MoveInput.y;
    }

    public void SetLocomotionAnimation(Vector2 value)
    {
        Animator.SetFloat(MoveXHash, value.x, animDamp, Time.deltaTime);
        Animator.SetFloat(MoveYHash, value.y, animDamp, Time.deltaTime);
    }

    public void SetVerticalVelocityAnim() => Animator.SetFloat(VerticalVelHash, VerticalVelocity);
    public void TriggerJumpAnim() => Animator.SetTrigger(JumpHash);

    public void ApplyGravity()
    {
        if (IsGrounded && VerticalVelocity < 0f) VerticalVelocity = -2f;  // giu sat dat
        else VerticalVelocity += gravity * Time.deltaTime;
    }

    public void Move()
    {

        //quãng đường đi đường mỗi frame 
        Controller.Move((HorizontalVelocity + Vector3.up * VerticalVelocity) * Time.deltaTime);
    }
}
