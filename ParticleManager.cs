using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private float durationTime = 0;

    private void Update()
    {
        if(durationTime >= 1)
        {
            return;
        }
        durationTime += Time.deltaTime;

        if(durationTime >= 0.9)
        {
            GetComponent<ParticleSystem>().Stop();
        }

        
    }

    private void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}
