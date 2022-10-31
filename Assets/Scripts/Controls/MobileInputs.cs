using NeoFPS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputs : InputCharacterMotion
{
    public static MobileInputs Instance;

    private void Awake()
    {
        base.OnAwake();
        if (Instance == null)
        {
            Instance = this;
            CheckMotionGraphConnection();
        }
    }

    protected override void UpdateInput()
    {
        //// Aim input
        m_Aimer.HandleMouseInput(LevelsManager.Instance.hud.GetTouchpadValues());

        // Movement input
        Vector2 move = LevelsManager.Instance.hud.GetJoystickValues();

        float mag = Mathf.Clamp01(move.magnitude);
        if (mag > Mathf.Epsilon)
            move.Normalize();

        m_Character.motionController.inputMoveDirection = move;
        m_Character.motionController.inputMoveScale = mag;
    }

    public void OnCrouch(bool crouch)
    {
        if (m_CrouchProperty != null)
        {
            m_CrouchProperty.SetInput(
                false, crouch
            );
        }
    }
}