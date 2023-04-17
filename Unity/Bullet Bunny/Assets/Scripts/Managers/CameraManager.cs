using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed = 0.125f;
    public float maxHorizontalDistance = 2.0f;
    private Vector3 playerPosition;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public bool useVerticalCamera;
    public bool useHorizontalCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (useVerticalCamera == true)
        {

            //Vertical
            playerPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);

            if (Mathf.Abs(transform.position.y - target.position.y) > maxHorizontalDistance)
            {
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, playerPosition, cameraSpeed);
                //transform.position = smoothedPosition;
                transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
            }

        }

        if (useHorizontalCamera == true)
        {

            //Horizontal
            playerPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

            if (Mathf.Abs(transform.position.x - target.position.x) > maxHorizontalDistance)
            {
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, playerPosition, cameraSpeed);
                //transform.position = smoothedPosition;
                transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
            }

        }
        
    }
}
