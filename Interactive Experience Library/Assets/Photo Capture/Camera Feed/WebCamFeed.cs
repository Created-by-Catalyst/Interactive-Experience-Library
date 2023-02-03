using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamFeed : MonoBehaviour
{
    Texture2D webcamTexture2D;

    int currentCameraIndex = 0;

    private WebCamTexture webcamTexture;

    public RawImage rawImage;

    public Texture2D WebcamTexture2D
    {
        get
        {
            return webcamTexture2D;
        }
    }


    [ContextMenu("List device names")]
    public void ListCameras()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (var i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }
    }

    [ContextMenu("Current Using Camera")]
    public void CurrentCam()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        Debug.Log(devices[currentCameraIndex].name);
    }


    private void Start()
    {
        PlayWebCamOnImage(currentCameraIndex);
    }

    public void StopCam()
    {
        webcamTexture.Stop();
    }

    public void PlaySpecificCam(string cameraName)
    {
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            if (WebCamTexture.devices[i].name == cameraName)
            {
                PlayWebCamOnImage(i);
            }
        }
    }


    void PlayWebCamOnImage(int cameraIndex)
    {

        webcamTexture = new WebCamTexture(WebCamTexture.devices[cameraIndex].name);

        rawImage.texture = webcamTexture;
        rawImage.material.mainTexture = webcamTexture;

        webcamTexture.Play();
    }

    [ContextMenu("Next camera")]
    public void NextCamera()
    {
        currentCameraIndex = ++currentCameraIndex % WebCamTexture.devices.Length;
        webcamTexture.Stop();
        PlayWebCamOnImage(currentCameraIndex);
    }
}
