using System.Collections;
using UnityEngine;

public class TransparencyCaptureToFile:MonoBehaviour
{
    private static uint _count = 0;
    
    public IEnumerator capture()
    {

        yield return new WaitForEndOfFrame();
        //After Unity4,you have to do this function after WaitForEndOfFrame in Coroutine
        //Or you will get the error:"ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame"
        zzTransparencyCapture.captureScreenshot("capture_" + _count + ".png");

        _count++;
        Debug.Log("Take a picture");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(capture());
    }
}