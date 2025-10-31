using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : carcontroller
{

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
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
            RearRightWheelCollider.brakeTorque = EngineBrake;
            RearLeftWheelCollider.brakeTorque = EngineBrake;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Unflip();
            RearLeftWheelCollider.motorTorque = 0;
            RearRightWheelCollider.motorTorque = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            EBrake();
        }
    }

    private void PlayerInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    }

    private void Unflip()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 rot = transform.eulerAngles;
        rot.z = 0f;
        transform.eulerAngles = rot;
        Speed = 0;
    }
}
