using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    GameObject playerPoint;
   
    // Start is called before the first frame update
    void Start()
    {
        playerPoint = GameObject.Find("PlayerPoint");
    }

    // Update is called once per frame
    void Update()
    {
        MoveMiniMapCamera();
    }

    void MoveMiniMapCamera()
    {
        this.transform.position = new Vector3(playerPoint.transform.position.x, playerPoint.transform.position.y, -100);
    }
}
