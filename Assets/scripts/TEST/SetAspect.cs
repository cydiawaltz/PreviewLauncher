using UnityEngine;

public class ForceAspect16by9 : MonoBehaviour
{
    int lastScreenHeight;

    void Start()
    {
        lastScreenHeight = Screen.height;
        Apply();
    }

    void Update()
    {
        if (Screen.height != lastScreenHeight)
        {
            lastScreenHeight = Screen.height;
            Apply();
        }
    }

    void Apply()
    {
        int height = Screen.height;
        int width = height * 16 / 9;

        Screen.SetResolution(width, height, Screen.fullScreenMode);
    }
}
