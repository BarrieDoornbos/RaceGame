using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : carcontroller
{
    private List<GameObject> AllCheckpoints = new List<GameObject>();
    private List<GameObject> CheckpointsList;
    private int CurrentCheckpoints;
    private int TotalCheckpoints;

    private List<GameObject> AllPlacementPoints = new List<GameObject>();
    private List<GameObject> PlacementPointsList;

    private int CurrentRound = 1;
    private int TotalRounds = 3;

    public TMP_Text CheckpointCounter;
    public TMP_Text RoundCounter;
    public TMP_Text Position;
    public TMP_Text FinishedPosition;

    public GameObject FinishedScreen;
    public GameObject EscapeMenu;

    public enemy Enemyscript1;
    public enemy enemyscript2;
    public enemy enemyscript3;

    int[] AllCheckpointsPassed;
    int playerIndex = 0;
    int placement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").ToList();
        AllPlacementPoints = GameObject.FindGameObjectsWithTag("PlacementPoint").ToList();

        PlacementPointsList = new List<GameObject>(AllPlacementPoints);
        CheckpointsList = new List<GameObject>(AllCheckpoints);

        TotalCheckpoints = AllCheckpoints.Count;
    }
    private void Update()
    {
        PlayerInput();
        Accelerate();
        Turn();
        UpdateAllWheels();

        AllCheckpointsPassed =new int[] { CheckpointsPassed, Enemyscript1.CheckpointsPassed, enemyscript2.CheckpointsPassed, enemyscript3.CheckpointsPassed};

        var CalculatePlacement = AllCheckpointsPassed.Select((place, index) => new { place, index }).OrderByDescending(s => s.place).ToList();

        placement = CalculatePlacement.FindIndex(n => n.index == playerIndex) + 1;

        CheckpointCounter.text = "Checkpoint: " + CurrentCheckpoints + "/" + TotalCheckpoints;
        RoundCounter.text = "Round: " + CurrentRound + "/" + 3;
        Position.text = "Position: " + placement + "/4";

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
            transform.Translate(Vector3.back * 10f * Time.deltaTime, Space.Self);
            RearLeftWheelCollider.motorTorque = 0;
            RearRightWheelCollider.motorTorque = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            EBrake();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            EscapeMenu.SetActive(true);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint" && CheckpointsList.Contains(other.gameObject))
        {
            CheckpointsList.Remove(other.gameObject);
            CurrentCheckpoints++;
        }

        if (other.gameObject.tag == "StartFinish" && CheckpointsList.Count == 0)
        {
            if (CurrentRound == TotalRounds)
            {
                Time.timeScale = 0;
                FinishedPosition.text = "Congratulations, you finished on position " + placement;
                FinishedScreen.SetActive(true);
            }
            else
            {
                CurrentRound++;
                CheckpointsList = new List<GameObject>(AllCheckpoints);
                PlacementPointsList = new List<GameObject>(AllPlacementPoints);
                CurrentCheckpoints = 0;
            }

        }

        if (other.gameObject.tag == "PlacementPoint" && PlacementPointsList.Contains(other.gameObject))
        {
            CheckpointsPassed++;
            PlacementPointsList.Remove(other.gameObject);
        }
    }
}
