using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class PhotoCapture : MonoBehaviour
{
    public int resWidth = 1080;

    public int resHeight = 1920;

    public bool useScreenResolution = false;

    public Camera captureCamera;

    public Texture2D image;

    //[SerializeField] DisplayPhotoCapture displayPhoto;
    public delegate void OnPhotoCaptured(Texture2D tex);

    public OnPhotoCaptured onPhotoCaptured;

    public static string CaptureName(int width, int height)
    {
        return string
            .Format("{0}/screenshots/{1}x{2}_{3}.jpg",
            Application.dataPath,
            width,
            height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    [ContextMenu("Take Screenshot")]
    public void Capture()
    {
        StartCoroutine(ICapture());
    }

    IEnumerator ICapture()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();
        RenderTexture rt =
            useScreenResolution
                ? new RenderTexture(Screen.width, Screen.height, 24)
                : new RenderTexture(resWidth, resHeight, 24);
        captureCamera.targetTexture = rt;
        Texture2D screenShot =
            useScreenResolution
                ? new Texture2D(Screen.width,
                    Screen.height,
                    TextureFormat.RGB24,
                    false)
                : new Texture2D(resWidth,
                    resHeight,
                    TextureFormat.RGB24,
                    false);
        captureCamera.Render();
        RenderTexture.active = rt;
        if (useScreenResolution)
        {
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        }
        else
        {
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        }
        captureCamera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy (rt);
        yield return new WaitForEndOfFrame();
        image = screenShot;
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Photo taken!");
        onPhotoCaptured?.Invoke(image);
    }

    public string SaveScreenShot()
    {
        byte[] bytes = image.EncodeToJPG();
        string filename = CaptureName(resWidth, resHeight);

        if (!Directory.Exists(Application.dataPath + "/screenshots/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/screenshots/");
        }

        File.WriteAllBytes (filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        Debug.Log (filename);
        return filename;
    }
}
