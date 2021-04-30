using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    public SoundManager soundManager;
 

    public void Awake()
    {
        if(GManager.instance == null)
        {
            Instantiate(gameManager);
        }
        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }

    }
}
