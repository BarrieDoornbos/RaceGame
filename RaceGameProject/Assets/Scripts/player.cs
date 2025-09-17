using UnityEngine;

public class player : carcontroller
{

    private void Update()
    {
        PlayerInput();
        Accelerate();
        Turn();
        UpdateAllWheels();
    }

    private void PlayerInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        IsBraking = Input.GetKey(KeyCode.Space);
    }
}
