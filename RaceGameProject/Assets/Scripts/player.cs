using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : carcontroller
{
    private void Update()
    {
        PlayerInput();
        Accelerate();
        Turn();
        UpdateAllWheels();

        Speed = rb.linearVelocity.magnitude * 3.6f;

        if (VerticalInput == 0 && Speed < 1)
        {
            Speed = 0;
        }

        if (Speed >= MaxSpeed)
        {
            Speed = MaxSpeed;
        }

        if (VerticalInput == 0 && Speed > 5)
        {
            RearRightWheelCollider.motorTorque -= EngineBrake;
            RearLeftWheelCollider.motorTorque -= EngineBrake;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Unflip();
            RearLeftWheelCollider.motorTorque = 0;
            RearRightWheelCollider.motorTorque = 0;
        }
    }

    private void PlayerInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        IsBraking = Input.GetKey(KeyCode.Space);
    }

    private void Unflip()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    }
}
