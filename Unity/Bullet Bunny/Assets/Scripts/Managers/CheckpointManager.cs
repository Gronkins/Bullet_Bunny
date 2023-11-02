using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint[] checkpoints;
    
    private void Awake()
    {
        checkpoints = GetComponentsInChildren<Checkpoint>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            bool isActive = GameManager.Instance.GetCheckpointState(checkpoint.checkpointID);
            checkpoint.isActive = isActive;
            checkpoint.InitialiseCheckpoint();
        }
    }
}
