using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;

public class Camera_Player : MonoBehaviour
{
    GameObject playerObj;
    Transform playerTransform;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] CinemachineConfiner cinemachineConfiner;
    void Start()
    {
        playerObj = GameObject.Find("PlayerAnimation");
        playerTransform = playerObj.transform;
        cinemachineVirtualCamera.Follow = playerTransform; 
    }

   
}
