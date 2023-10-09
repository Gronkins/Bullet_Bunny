using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelScroll: MonoBehaviour
{
    public Transform level;
    public float scrollSpeed = 1.0f;
    public float spawnThreshold = 10.0f; // Adjust this value based on your level's size

    private Transform duplicateLevel;
    private float levelWidth;

    private void Start()
    {
        levelWidth = 42;
        duplicateLevel = Instantiate(level, level.position + new Vector3(levelWidth, 0, 0), Quaternion.identity);
    }

    private void Update()
    {
        level.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        duplicateLevel.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (level.position.x <= -levelWidth)
        {
            // Move level to the right to prepare for duplication
            level.position += new Vector3(levelWidth, 0, 0);
            
            // Spawn a duplicate of the level at the end
            Destroy(duplicateLevel.gameObject);
            duplicateLevel = Instantiate(level, level.position + new Vector3(levelWidth, 0, 0), Quaternion.identity);
        }
    }
}
