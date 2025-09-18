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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Unflip();
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
