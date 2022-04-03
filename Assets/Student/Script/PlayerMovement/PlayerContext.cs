using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using System.Collections.Generic;

public class PlayerContext : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    private IPlayerMoveState currState;
    private IPlayerMoveState oldState;

    [SerializeField] private ParticleSystem psWallSlide;

    [Header("Rigidbody & Colliders")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D feetCol;
    [SerializeField] private Collider2D grabCol;
    [SerializeField] private Collider2D shouldercol;
    [SerializeField] private Transform grabbedObjectTR;
    private GameObject grabbedObject = null;
    private GameObject grabableObject = null;    


    [Header("Inputs")]
    [SerializeField] private float moveFactor = 150.0f;
    [SerializeField] private float jumpFactor = 350.0f;
    private float inputDirH;
    private bool inputJump = false;
    private bool inputGrab = false;
    private bool inputThrow = false;
    private bool inputMischief = false;
    private bool inputOption = false;

    [Header("Aniamtor")]
    [SerializeField] private Animator anim;


    // SECTION - Property --------------------------------------------------------------------
    #region REGION - PROPERTY
    //public IPlayerMoveState CurrState { get => currState; set => currState = value; }

    public ParticleSystem PsWallSlide { get => psWallSlide; set => psWallSlide = value; }

    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public Collider2D FeetCol { get => feetCol; set => feetCol = value; }
    public Collider2D GrabCol { get => grabCol; set => grabCol = value; }
    public GameObject GrabbedObject { get => grabbedObject; set => grabbedObject = value; }
    public GameObject GrabableObject { get => grabableObject; set => grabableObject = value; }
    public Collider2D Shouldercol { get => shouldercol; set => shouldercol = value; }
    public Transform GrabbedObjectTR { get => grabbedObjectTR; set => grabbedObjectTR = value; }

    public float MoveFactor { get => moveFactor;}
    public float JumpFactor { get => jumpFactor;}
    public float InputDirH { get => inputDirH; set => inputDirH = value; }
    public bool InputJump { get => inputJump; set => inputJump = value; }
    public bool InputGrab { get => inputGrab; set => inputGrab = value; }
    public bool InputMischief { get => inputMischief; set => inputMischief = value; }
    public bool InputThrow { get => inputThrow; set => inputThrow = value; }
    public bool InputOption { get => inputOption; set => inputOption = value; }

    public Animator Anim { get => anim; set => anim = value; }
    #endregion


    // SECTION - Method - Unity -------------------------------------------------------------------
    private void Start()
    {
        currState = new PlayerGroundedState();
        oldState = currState;
    }

    private void FixedUpdate()
    {
        if (oldState != currState)
        {
            oldState = currState;
            currState.OnStateEnter(this);
        }

        OnStateUpdate();
        OnStateExit();
    }

    // SECTION - Method - Context Related -------------------------------------------------------------------
    public void FlipAll()
    {
        if (this.inputDirH < 0 && this.transform.localScale.x != -1.0f) // <- Flip All
            this.transform.localScale = new Vector2(-1.0f, 1.0f);
        else if (this.inputDirH > 0 && this.transform.localScale.x != 1.0f) // -> Flip All
            this.transform.localScale = new Vector2(1.0f, 1.0f);
    }

    public void AddRigidActivateColl()
    {
        if (!grabbedObject.GetComponent<Rigidbody2D>())
        {
            grabbedObject.GetComponent<Collider2D>().enabled = true;
            grabbedObject.AddComponent<Rigidbody2D>();
        }
    }


    // SECTION - Method - State Machine -------------------------------------------------------------------
    public void OnMove()
    {
        currState.Move(this);
    }

    public void OnJump()
    {
        currState.Jump(this);
    }

    public void OnGrabObject()
    {
        currState.Grab(this);
    }


    public void OnStateEnter()
    {
        currState.OnStateEnter(this);
    }

    public void OnStateUpdate()
    {
        currState.OnStateUpdate(this);
    }

    public void OnStateExit()
    {
        currState = currState.OnStateExit(this);
    }

}
