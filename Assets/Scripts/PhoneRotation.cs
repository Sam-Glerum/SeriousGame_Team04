using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneRotation : MonoBehaviour
{
    public bool played = false;
    public float shakeSpeed;
    public AudioClip shakeSound;
    AudioSource audioSource;
    public float GyroX = 0;
    float y = 0;


    void Start()
    {

        Input.gyro.enabled = true;
        Input.compass.enabled = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GyroX < Input.gyro.rotationRate.y)
        {

        }
            GyroX = Input.gyro.rotationRate.y;


            y = Input.gyro.rotationRate.y;
        {
            if (Mathf.Abs(Input.gyro.rotationRate.y) > shakeSpeed && !played)
            {
                audioSource.PlayOneShot(shakeSound);
                played = true;
            }
        }
    }
}
