using UnityEngine;

/// <summary : TODO_LIST>
/// 
/// TODO
///     - Rework ground/wall/push checks
///     - Refactorise Mischief() for interesting usage instead of current uselessness
///     - Implement walljump -if time-
///     
/// </summary>

public class PlayerGroundedState : IPlayerMoveState
{
    // SECTION - Field -------------------------------------------------------------------
    private const float mischiefFactor = 50.0f;


    // SECTION - Method - General -------------------------------------------------------------------
    public void Move(PlayerContext context)
    {
        // Flip all
        context.FlipAll();

        // Movement
        context.Rb.velocity = new Vector2(context.InputDirH * context.MoveFactor * Time.fixedDeltaTime, context.Rb.velocity.y);
        context.Anim.SetFloat("moveMagnitude", context.Rb.velocity.magnitude);

        // Set Pushing animation
        if (context.Rb.velocity.magnitude > 0.1 && 
           (context.FeetCol.IsTouchingLayers(LayerMask.GetMask("InteractiveObject")) || context.FeetCol.IsTouchingLayers(LayerMask.GetMask("Ground")) ) && 
            context.Shouldercol.IsTouchingLayers(LayerMask.GetMask("InteractiveObject"))         )
                context.Anim.SetBool("isPushing", true);
        else if (context.Anim.GetBool("isPushing"))
            context.Anim.SetBool("isPushing", false);
    }

    public void SlideWall(PlayerContext context)
    {
        // No Implementation in this.State
    }

    public void Jump(PlayerContext context)
    {
        // Jump
        if (context.InputJump)
        {
            context.Rb.AddForce(Vector2.up * context.JumpFactor * Time.fixedDeltaTime, ForceMode2D.Impulse);
            context.InputJump = false;

            context.Anim.SetBool("isJumping", true);
        }

    }

    public void Fall(PlayerContext context) // Not the prettiest code, but it works
    {
        if ((!context.Anim.GetBool("isJumping") && !context.FeetCol.IsTouchingLayers(LayerMask.GetMask("Ground"))) &&
            (!context.Anim.GetBool("isJumping") && !context.FeetCol.IsTouchingLayers(LayerMask.GetMask("InteractiveObject"))))
            context.Anim.SetBool("isFalling", true);
    }

    public void Grab(PlayerContext context)
    {
        if (context.InputGrab && context.GrabbedObject == null && context.GrabableObject != null) // Grab
        {
            context.GrabbedObject = context.GrabableObject;

            Rigidbody2D rbTemp;
            if (context.GrabbedObject.TryGetComponent<Rigidbody2D>(out rbTemp))
            {
                Object.Destroy(context.GrabbedObject.GetComponent<Rigidbody2D>());
                context.GrabbedObject.GetComponent<Collider2D>().enabled = false;
            }

            // Set position
            context.GrabbedObject.transform.parent = context.transform;
            context.GrabbedObject.transform.position = context.GrabbedObjectTR.position;

            context.GrabableObject = null;
        }
        else if (context.InputGrab && context.GrabbedObject != null) // Drop
        {
            // Set position
            context.GrabbedObject.transform.parent = null;

            context.AddRigidActivateColl();

            // Continuous rb detect : High drop passes through ground otherwise
            context.GrabbedObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            context.GrabbedObject = null;
        }

        context.InputGrab = false;
    }

    public void Throw(PlayerContext context)
    {
        if (context.InputThrow && context.GrabbedObject != null)
        {
            context.GrabbedObject.transform.parent = null;

            context.AddRigidActivateColl();

            // High drop passes through ground otherwise
            context.GrabbedObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            // Get direction for throw
            float localX = context.transform.localScale.x;
            float localY = context.transform.localScale.y;

            // Throw
            context.GrabbedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(localX * 3, localY), ForceMode2D.Impulse);

            context.GrabbedObject = null;
        }
    }


    // TODO : Refactorise Mischief() for interesting usage instead of current uselessness
    public void Mischief(PlayerContext context)
    {
        if (context.InputMischief && context.GrabableObject != null)
        {
            context.Anim.SetBool("isMischieving", true);

            // Get direction for push
            float localX = context.transform.localScale.x;
            float localY = context.transform.localScale.y;

            context.GrabableObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(localX * mischiefFactor, localY), ForceMode2D.Force);
        }
    }

    public void OnStateEnter(PlayerContext context)
    {
        Debug.Log("Enter Grounded State");
    }

    public void OnStateUpdate(PlayerContext context)
    {
        Move(context);
        SlideWall(context);
        Jump(context);
        Fall(context);
        Grab(context);
        Throw(context);
        Mischief(context);
    }

    public IPlayerMoveState OnStateExit(PlayerContext context)
    {
        // return state checks
        if ( context.InputJump || (!context.FeetCol.IsTouchingLayers(LayerMask.GetMask("Ground")) && !context.FeetCol.IsTouchingLayers(LayerMask.GetMask("InteractiveObject"))) )
            return new PlayerAirborneState();

        return this;
    }
}
