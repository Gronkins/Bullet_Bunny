using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    private FallingPlatformDustParticles dustParticles;
    
    private void Awake()
    {
        dustParticles = GetComponentInChildren<FallingPlatformDustParticles>();
    }

    public void SetFalling()
    {
        dustParticles.isFalling = true;
    }
}
