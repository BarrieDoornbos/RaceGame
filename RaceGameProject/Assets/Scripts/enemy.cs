using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemy : carcontroller
{
    private List<GameObject> CheckpointsList = new List<GameObject>();
    private List<GameObject> Checkpoints;

    private void Start()
    {
        CheckpointsList = GameObject.FindGameObjectsWithTag("Checkpoint").ToList();
        Checkpoints = new List<GameObject>(CheckpointsList);
    }

    private void NextCheckpoint()
    {
        foreach (var checkpoint in Checkpoints)
        {

        }
            
    }
}
