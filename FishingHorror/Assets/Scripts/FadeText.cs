using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    //field to hold this object's text mesh component
    private TextMesh text;

    //field for fade direction bool
    public bool fadeDirection = false;

    //fields for fade time and time incremention
    public float fadeTime;
    private float fadeTick;

    //fields for transparent and opaque colors
    private Color transparent;
    private Color opaque;

    // Start is called before the first frame update
    void Start()
    {
        //store text mesh
        text = gameObject.GetComponent<TextMesh>();

        //store transparent and opaque colors
        transparent = text.color;
        opaque = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //check if fade tick is less than 1
        if(fadeTick <= 1)
        {
            //fade the text if it hasn't been faded already
            if (fadeDirection == true)
            {
                //lerp text's transparency to make it visible
                text.color = Color.Lerp(text.color, opaque, fadeTick);

                //debug for fading text
                Debug.Log("Text has faded in");
            }
            else if(fadeDirection == false)
            {
                //lerp text's transparency to make it invisible
                text.color = Color.Lerp(text.color, transparent, fadeTick);

                //debug for fading text
                Debug.Log("Text has faded out");
            }

            //increment fade tick
            fadeTick += Time.deltaTime / fadeTime;
        }
    }

    public void TextFade(bool setFade)
    {
        //set direction of fade
        fadeDirection = setFade;

        //reset fade tick
        fadeTick = 0f;
    }
}
