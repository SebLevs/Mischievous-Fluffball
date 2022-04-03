using UnityEngine;


/// <summary : TODO_LIST>
/// 
/// TODO
///     - Rework ground/wall/push checks
///     - Refactorise Mischief() for interesting usage instead of current uselessness
///     - Implement walljump -if time-
/// 
/// </summary>

public class PlayerAirborneState : IPlayerMoveState
{
    const float psPivotFlipX = -0.5f;

    // SECTION - Method - General -------------------------------------------------------------------
    public void Move(PlayerContext context)
    {
        // Flip all
        context.FlipAll();

        // Movement slowed while mid-air
        context.Rb.velocity = new Vector2(context.InputDirH * (float)((int)context.MoveFactor >> 1) * Time.fixedDeltaTime, context.Rb.velocity.y);

        context.Anim.SetFloat("moveMagnitude", context.Rb.velocity.magnitude);
    }

    public void SlideWall(PlayerContext context)
    {
        if (context.Shouldercol.IsTouchingLayers(LayerMask.GetMask("Wall")) && context.Rb.velocity.x != 0)
        {
            context.Anim.SetBool("isJumping", false);
            context.Anim.SetBool("isFalling", false);
            context.Anim.SetBool("isSlidingWall", true);
        }
        else if (context.Anim.GetBool("isSlidingWall") && !context.Shouldercol.IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            context.Anim.SetBool("isSlidingWall", false);
            context.Anim.SetBool("isFalling", true);
        }

        // Particle Effects flip on wall slide
        ParticleSystemRenderer psRend = context.PsWallSlide.GetComponent<ParticleSystemRenderer>();
        if (context.transform.localScale.x < 0 && psRend.pivot.x == 0)
            psRend.pivot = new Vector3 (psPivotFlipX, 0.0f, 0.0f);
        else if (context.transform.localScale.x > 0 && psRend.pivot.x != 0)
            psRend.pivot = new Vector3(0.0f, 0.0f, 0.0f);

        
        context.PsWallSlide.transform.position = context.Shouldercol.transform.position;


        if (context.Anim.GetBool("isSlidingWall") && !context.PsWallSlide.isPlaying && context.InputDirH == 0)
            context.PsWallSlide.Play();
        else if (context.InputDirH != 0 && context.PsWallSlide.isPlaying)
            context.PsWallSlide.Stop();
    }


    public void Jump(PlayerContext context)
    {
        // For double jump on wall - Not implemented -
    }

    public void Fall(PlayerContext context)
    {
        if (!context.FeetCol.IsTouchingLayers(LayerMask.GetMask("Ground")) && !context.FeetCol.IsTouchingLayers(LayerMask.GetMask("InteractiveObject")) && !context.Shouldercol.IsTouchingLayers(LayerMask.GetMask("Wall")))
            context.Anim.SetBool("isFalling", true);
    }

    public void Grab(PlayerContext context)
    {
        if (context.InputGrab && context.GrabbedObject != null) // Drop
        {
            context.GrabbedObject.transform.parent = null;

            context.AddRigidActivateColl();

            // High drop passes through ground otherwise
            context.GrabbedObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            context.GrabbedObject = null;
        }
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
            context.GrabbedObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(localX * 2, localY), ForceMode2D.Impulse);

            context.GrabbedObject = null;
        }
    }

    public void Mischief(PlayerContext context)
    {
        // Does nothing as fluffball shouldn't be able to mischief in midair
    }


    public void OnStateEnter(PlayerContext context)
    {
        Debug.Log("Enter Airborne State");
        context.InputJump = false;
    }

    public void OnStateUpdate(PlayerContext context)
    {
        Move(context);
        SlideWall(context);
        //Jump(context); // NOTE : Unused for now
        Fall(context);
        Grab(context);
        Throw(context);
    }

    public IPlayerMoveState OnStateExit(PlayerContext context)
    {  
        if (context.FeetCol.IsTouchingLayers(LayerMask.GetMask("Ground")) || context.FeetCol.IsTouchingLayers(LayerMask.GetMask("InteractiveObject")))
        {
            context.Anim.SetBool("isJumping", false);
            context.Anim.SetBool("isFalling", false);
            context.Anim.SetBool("isSlidingWall", false);

            context.PsWallSlide.Stop();
            context.PsWallSlide.transform.parent = null;

            return new PlayerGroundedState();
        }

        return this;
    }
}
