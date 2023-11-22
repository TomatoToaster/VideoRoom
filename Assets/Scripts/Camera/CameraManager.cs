using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<GameObject> screens;
    public List<GameObject> linkedCams;

    // Start is called before the first frame update
    void Start()
    {
        if (screens.Count == 0) {
            Debug.LogWarning("No screens yet");
        }
        if (screens.Count != linkedCams.Count) {
            Debug.LogWarning("screens and linked cams count mismatch");
        }
    }

    public int GetCount()
    {
        return screens.Count;
    }

    public void AddScreenAndCam(GameObject screen, GameObject cam)
    {
        screens.Add(screen);
        linkedCams.Add(cam);
        LinkCamToScreen(screen, cam);
    }

    public void RemoveScreenAndCam(int index)
    {
        screens.RemoveAt(index);
        linkedCams.RemoveAt(index);
    }

    public void GrabScreenAndCam(int index, out GameObject screen, out GameObject cam)
    {
        screen = screens[index];
        cam = linkedCams[index];
    }

    public void ReplaceCamAtScreen(int screenIndex, GameObject newCam)
    {
        GameObject oldCamera = linkedCams[screenIndex];
        linkedCams[screenIndex] = newCam;
        LinkCamToScreen(screens[screenIndex], newCam);
        Destroy(oldCamera);
    }

    private void LinkCamToScreen(GameObject screen, GameObject cam)
    {
        screen.GetComponentInChildren<ScreenAbsorb>().LinkCam(cam);
    }
}
