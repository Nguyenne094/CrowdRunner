using TMPro;
using UnityEngine;

public class FPSShow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float fps = (1f / Time.deltaTime);
        GetComponent<TextMeshProUGUI>().text = "FPS: " + fps;
    }
}
