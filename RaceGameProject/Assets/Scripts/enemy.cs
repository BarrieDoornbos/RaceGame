using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : carcontroller
{
    public List<GameObject> CheckpointsList = new List<GameObject>();
    private List<GameObject> Checkpoints;
    public GameObject ClosestCheckpoint;

    private void Start()
    {
        Checkpoints = new List<GameObject>(CheckpointsList);
        ClosestCheckpoint = Checkpoints.First();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Accelerate();
        Turn();
        UpdateAllWheels();

        Speed = rb.linearVelocity.magnitude * 3.6f;

        Vector3 DirToMove = (ClosestCheckpoint.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, DirToMove);

        if (Speed >= MaxSpeed)
        {
            Speed = MaxSpeed;
        }

        if (Checkpoints.Count == 0)
        {
            ClosestCheckpoint = GameObject.FindWithTag("StartFinish");
        }

        if (dot > 0)
        {
            VerticalInput = 1;
        }
        else
        {
            VerticalInput = -1;
        }

        float AngleToDir = Vector3.SignedAngle(transform.forward, DirToMove, transform.up);

        if (AngleToDir > 8)
        {
            HorizontalInput = 1;
        }
        else if (AngleToDir < -8)
        {
            HorizontalInput = -1;
        }
        else
        {
            HorizontalInput = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint" && Checkpoints.Contains(other.gameObject))
        {
            Checkpoints.Remove(other.gameObject);
            ClosestCheckpoint = Checkpoints.First();
        }

        if (other.gameObject.tag == "StartFinish" && Checkpoints.Count == 0)
        {
            Checkpoints = new List<GameObject>(CheckpointsList);
            ClosestCheckpoint = Checkpoints.First();
        }

        if (other.gameObject.tag == "AiBrake" && Speed >= 50)
        {
            IsBraking = true;
        }
        else
        {
            IsBraking = false;
        }
    }
}
