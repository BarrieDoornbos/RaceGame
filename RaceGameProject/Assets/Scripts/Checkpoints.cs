using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class Checkpoints : MonoBehaviour
{
    private List<GameObject> AllCheckpoints = new List<GameObject>();
    private List<GameObject> CheckCheckpoints;
    private int CurrentCheckpoints;
    private int TotalCheckpoints;

    private int CurrentRound = 1;
    private int TotalRounds = 3;
    private int PlayableRounds = 3;

    public TMP_Text CheckpointCounter;
    public TMP_Text RoundCounter;

    private void Start()
    {
        AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").ToList();
        CheckCheckpoints = new List<GameObject>(AllCheckpoints);
        TotalCheckpoints = AllCheckpoints.Count;
    }

    private void Update()
    {
        CheckpointCounter.text = "Checkpoint: " + CurrentCheckpoints + "/" + TotalCheckpoints;
        RoundCounter.text = "Round: " + CurrentRound + "/" + PlayableRounds;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint" && CheckCheckpoints.Contains(other.gameObject))
        {
            CheckCheckpoints.Remove(other.gameObject);
            CurrentCheckpoints++;
        }

        if (other.gameObject.tag == "StartFinish" && CheckCheckpoints.Count == 0)
        {
            if (CurrentRound == TotalRounds)
            {
                Time.timeScale = 0;
            } else
            {
                CurrentRound++;
                CheckCheckpoints = new List<GameObject>(AllCheckpoints);
                CurrentCheckpoints = 0;
            }

        }
    }
}
