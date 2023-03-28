using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;  // Weather or not the screen should fade in on scene start
    public float defaultFadeDuration = 2;   // length of fade effect in seconds
    public Color fadeColor;                 // current [R,G,B,A] of fade screen
    private Renderer _rend;                 // Fade screen renderer
    private static readonly int Color1 = Shader.PropertyToID("_Color"); // Color parameter from default URP lit shader

    // Start is called before the first frame update
    void Start()
    {
        _rend = GetComponent<Renderer>();       // Get fade screen renderer
        if (fadeOnStart)                        // Fade screen on scene start
            FadeIn();                          
    }

    public void Fade(float alphaIn, float alphaOut)         // Interpolate between two alfa values
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, defaultFadeDuration));    
    }
    
    public void Fade(float alphaIn, float alphaOut, float fadeDuration) // Overload with specified duration
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, fadeDuration));
    }

    public void FadeIn()        // Fixed fade in function
    {
        Fade(1,0);
    }

    public void FadeOut()      // Fixed fade out function
    {
        Fade(0,1);
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut, float fadeDuration) // Interpolation co-routine
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor; // retrieve 
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration); // Interpolate value slightly towards alpha level
            _rend.material.SetColor(Color1, newColor); // update to current alpha level of fade screen
            
            timer += Time.deltaTime; // Update timer
            yield return null;
        }
        // Once the majority of the fade is completed lock the current alpha value to the alpha out value 
        Color newColor2 = fadeColor; 
        newColor2.a = alphaOut;
        _rend.material.SetColor(Color1, newColor2); // Update final color value
        
    }
}
