using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Camera;
    public Transform Player;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = Player.position - Camera.transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0.0f);
        Debug.DrawRay(Camera.transform.position, newDirection, Color.red);
        Camera.transform.rotation = Quaternion.LookRotation(newDirection);


        Vector3 position = Camera.transform.position;
        position.y = Player.position.y+1.5f;
        Camera.transform.position = position;

    }
}
