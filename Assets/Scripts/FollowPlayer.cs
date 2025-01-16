using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 2, -5);
    private PlayerController playerControllerScript;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        if(playerControllerScript.isRotating)
        {
            //Debug.Log("FollowPlayer knows the camera is rotating.");
            offset = Quaternion.Euler(0, playerControllerScript.rotationInput * playerControllerScript.rotationSpeed, 0) * offset;
            transform.position = player.transform.position + offset;            
            Vector3 cameraTarget = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z); 
            transform.LookAt(cameraTarget);
        }
    }
}
