using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : carcontroller
{
    public List<GameObject> CheckpointsList = new List<GameObject>();
    private List<GameObject> Checkpoints;
    public GameObject ClosestCheckpoint;

    private List<GameObject> AllPlacementPoints = new List<GameObject>();
    private List<GameObject> PlacementPointsList;

    float stuckTimer = 2f;
    Vector3 offset;

    public bool AiBrakes = true;

    private void Start()
    {
        Checkpoints = new List<GameObject>(CheckpointsList);

        ClosestCheckpoint = Checkpoints.First();

        rb = GetComponent<Rigidbody>();

        offset = UnityEngine.Random.insideUnitSphere * 1.5f;
        offset.y = 0;

        AllPlacementPoints = GameObject.FindGameObjectsWithTag("PlacementPoint").ToList();
        PlacementPointsList = new List<GameObject>(AllPlacementPoints);
    }

    private void Update()
    {
        Accelerate();
        Turn();
        UpdateAllWheels();

        Speed = rb.linearVelocity.magnitude * 3.6f;

        Vector3 targetPos = ClosestCheckpoint.transform.position + offset;
        Vector3 DirToMove = (targetPos - transform.position).normalized;

        VerticalInput = 1;

        if (Speed >= MaxSpeed)
        {
            Speed = MaxSpeed;
        }

        if (Checkpoints.Count == 0)
        {
            ClosestCheckpoint = GameObject.FindWithTag("StartFinish");
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

        if (Speed < 3)
        {
            stuckTimer -= Time.deltaTime;
            if (stuckTimer < 0)
            {
                transform.Translate(Vector3.back * 10f * Time.deltaTime, Space.Self);
            }
        }
        else
        {
            stuckTimer = 2f;
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
            PlacementPointsList = new List<GameObject>(AllPlacementPoints);
        }

        if (AiBrakes && other.gameObject.tag == "AiBrake" && Speed >= 50)
        {
            IsBraking = true;
        }
        else
        {
            IsBraking = false;
        }

        if (other.gameObject.tag == "PlacementPoint" && PlacementPointsList.Contains(other.gameObject))
        {
            PlacementPointsList.Remove(other.gameObject);
            CheckpointsPassed++;
        }
    }
}
