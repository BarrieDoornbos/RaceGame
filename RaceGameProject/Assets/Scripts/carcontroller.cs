using Unity.Mathematics;
using UnityEngine;

public class carcontroller : MonoBehaviour
{
    public float MotorForce = 100f;
    public float BrakeForce = 500f;
    public float MaxSteeringAngle = 30f;

    public WheelCollider FrontLeftWheelCollider;
    public WheelCollider FrontRightWheelCollider;
    public WheelCollider RearLeftWheelCollider;
    public WheelCollider RearRightWheelCollider;

    public Transform FrontLeftWheel;
    public Transform FrontRightWheel;
    public Transform RearLeftWheel;
    public Transform RearRightWheel;

    protected float HorizontalInput;
    protected float VerticalInput;
    protected float CurrentSteeringAngle;
    protected float CurrentBrakeForce;
    protected bool IsBraking;

    protected void Accelerate()
    {
        RearLeftWheelCollider.motorTorque = VerticalInput * MotorForce;
        RearRightWheelCollider.motorTorque = VerticalInput * MotorForce;

        CurrentBrakeForce = IsBraking? BrakeForce : 0f;
        Brake();
    }

    protected void Turn()
    {
        CurrentSteeringAngle = MaxSteeringAngle * HorizontalInput;
        FrontLeftWheelCollider.steerAngle = CurrentSteeringAngle;
        FrontRightWheelCollider.steerAngle = CurrentSteeringAngle;
    }

    protected void Brake()
    {
        FrontLeftWheelCollider.brakeTorque = CurrentBrakeForce;
        FrontRightWheelCollider.brakeTorque = CurrentBrakeForce;
        RearLeftWheelCollider.brakeTorque = CurrentBrakeForce;
        RearRightWheelCollider.brakeTorque = CurrentBrakeForce;
    }

    protected void UpdateWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 Pos;
        Quaternion Rot;
        WheelCollider.GetWorldPose(out Pos, out Rot);
        WheelTransform.position = Pos;
        WheelTransform.rotation = Rot;
    }

    protected void UpdateAllWheels()
    {
        UpdateWheel(FrontRightWheelCollider, FrontRightWheel);
        UpdateWheel(FrontLeftWheelCollider, FrontLeftWheel);
        UpdateWheel(RearRightWheelCollider, RearRightWheel);
        UpdateWheel(RearLeftWheelCollider, RearLeftWheel);
    }
}