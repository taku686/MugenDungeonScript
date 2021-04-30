using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource se;

    [SerializeField] private float low = 0.95f;
    [SerializeField] private float high = 1.05f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    public void PlaySingle(AudioClip clip)
    {
        se.clip = clip;
        se.Play();
    }

    public void RandomSE(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(low, high);
        se.pitch = randomPitch;
        se.clip = clips[randomIndex];
        se.Play();
    }
}
