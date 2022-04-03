using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerMoveState
{
    /// <summary : State Pattern>
    ///
    /// State machine for Player Movements
    /// 
    /// Machine delegates behavior through Player Context class
    /// First time trying state pattern for player movement, code may or may not be spagettied
    /// 
    /// </summary>


    // SECTION - Method - General -------------------------------------------------------------------
    public void Move(PlayerContext context);
    public void SlideWall(PlayerContext context);
    public void Jump(PlayerContext context);
    public void Fall(PlayerContext context);
    public void Grab(PlayerContext context);
    public void Throw(PlayerContext context);
    public void Mischief(PlayerContext context);


    public void OnStateEnter(PlayerContext context);
    public void OnStateUpdate(PlayerContext context);
    public IPlayerMoveState OnStateExit(PlayerContext context);
}
