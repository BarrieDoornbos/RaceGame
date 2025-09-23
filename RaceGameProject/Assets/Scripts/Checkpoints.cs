using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Checkpoints : MonoBehaviour
{
    public List<GameObject> AllCheckpoints = new List<GameObject>();

    private void Start()
    {
        AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").ToList();
    }
}
