using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    float _horiznotal;
    bool  getAttack = false;

    public bool getPickUp;

    public static PlayerInput instance;

    void Awake()
    {
        instance = this;
    }

    public float horizontal()
    {
        return _horiznotal;
    }

    public bool attackInput()
    {
        return getAttack;
    }

    //=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=//=// 
    #region "Get들" 
    public void GetHorizontal(InputAction.CallbackContext context)
    {
        if (context.canceled || context.performed)
        {
            _horiznotal = context.ReadValue<Vector2>().x;
        }
    }

    public void GetAttack(InputAction.CallbackContext context)
    {
        //if(context.ReadValue<bool>())

        if (context.canceled || context.performed)
        {
            getAttack = !getAttack;
        }
    }

    public void GetPickUp(InputAction.CallbackContext context)
    {
        if(context.started || context.performed) getPickUp = true;
        else getPickUp = false;
    }

    #endregion
}
