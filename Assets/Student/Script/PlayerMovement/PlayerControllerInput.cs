using UnityEngine;
using UnityEngine.InputSystem;
using System.Drawing;
using System;
using UnityEngine.UI;

public class PlayerControllerInput : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [SerializeField] private PlayerContext context;

    [SerializeField] private CanvasRenderer uiSprite;
    [SerializeField] private Sprite[] possibleUiSprites;

    // SECTION - Method --------------------------------------------------------------------
    public void OnMove(InputAction.CallbackContext cbc)
    {
        context.InputDirH = cbc.ReadValue<Vector2>().x;
        SetKeyLayout(cbc);
    }

    public void OnJump(InputAction.CallbackContext cbc)
    {
        context.InputJump = cbc.performed;
        SetKeyLayout(cbc);
    }

    // TODO - Finish implementation or get rid of it
    public void OnGrab(InputAction.CallbackContext cbc)
    {
        context.InputGrab = cbc.performed;
        SetKeyLayout(cbc);
    }

    public void OnThrow(InputAction.CallbackContext cbc)
    {
        context.InputThrow = cbc.performed;
        SetKeyLayout(cbc);
    }

    public void OnMischief(InputAction.CallbackContext cbc)
    {
        context.InputMischief = cbc.performed;
        SetKeyLayout(cbc);
    }

    public void OnOption(InputAction.CallbackContext cbc)
    {
        context.InputOption = cbc.performed;
        SetKeyLayout(cbc);
    }

    public void SetKeyLayout(InputAction.CallbackContext cbc)
    {
        // Keyboard
        if (InputControlPath.MatchesPrefix("<Keyboard>", cbc.control))
            uiSprite.GetComponent<Image>().sprite = possibleUiSprites[0];

        // Gamepad
        else if (InputControlPath.MatchesPrefix("<GamePad>", cbc.control))
            uiSprite.GetComponent<Image>().sprite = possibleUiSprites[1];
    }
}
