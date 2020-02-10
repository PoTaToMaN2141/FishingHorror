using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //field for camera's start position
    private Vector3 startPosition;

    //fields for start, end, current, and decrement shake intensity
    public float startIntensity;
    public float endIntensity;
    private float currentIntensity;
    private float decrementIntensity;

    //field for shake time and and shake tick
    public float shakeTime;
    private float shakeTick;

    //field for shake bool
    private bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shake the screen if shakebool is true
        if(isShaking == true)
        {
            //check if the shake time has passed
            if(shakeTick <= 1)
            {
                //get random values to move camera left and right based on current intensity and clamp them as needed
                float shakeX = Random.Range(-currentIntensity, currentIntensity);
                float shakeY = Random.Range(-currentIntensity, currentIntensity);

                //get new x and y positoion values and clamp them as needed
                float transformX = transform.localPosition.x + shakeX;
                transformX = Mathf.Clamp(transformX, -0.1f, 0.1f);
                float transformY = transform.localPosition.y + shakeY;
                transformY = Mathf.Clamp(transformY, -0.1f, 0.1f);

                //create new vector3 with random shake values
                Vector3 shakeVector = new Vector3(transformX, transformY, transform.localPosition.z);

                //set camera's position based on the shakevector
                transform.localPosition = shakeVector;

                //add to current shake intensity
                currentIntensity -= decrementIntensity * Time.deltaTime;

                //increment shake tick
                shakeTick += Time.deltaTime / shakeTime;
            }
            else
            {
                //reset camera's position
                transform.localPosition = Vector3.zero;

                //set shaking bool to false
                isShaking = false;
            }
        }
        else
        {
            //check for debug input to test screenshake
            if (Input.GetKeyDown(KeyCode.P))
            {
                //start shaking the screen
                Shake(startIntensity, endIntensity, shakeTime);
            }
        }
    }

    /// <summary>
    /// method that sets up how the screen will shake 
    /// </summary>
    /// <param name="startIntensity"> the starting intensity of the shake </param>
    /// <param name="endIntensity"> the ending intensity of the shake </param>
    public void Shake(float shakeStart, float shakeEnd, float time)
    {
        //set start and end intensities based on passed-in values
        startIntensity = shakeStart;
        endIntensity = shakeEnd;

        //set shake time
        shakeTime = time;

        //set current intensity to start intensity
        currentIntensity = startIntensity;

        //get the proper decrementation value based on intensities and shake time
        decrementIntensity = (startIntensity - endIntensity) / shakeTime;

        //reset shake tick
        shakeTick = 0f;

        //set shake bool to true
        isShaking = true;
    }
}
