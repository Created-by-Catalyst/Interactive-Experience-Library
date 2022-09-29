using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPhotoCapture : MonoBehaviour
{
    [SerializeField]
    PhotoCapture photoCapture;

    [Space]
    [SerializeField]
    RawImage raw_image;

    private void OnEnable()
    {
        photoCapture.onPhotoCaptured += DisplayImage;
    }

    private void OnDisable()
    {
        photoCapture.onPhotoCaptured -= DisplayImage;
    }

    public void DisplayImage(Texture2D image)
    {
        if (image != null)
        {
            image.Apply();
            raw_image.texture = image;
            Debug.Log("Material's texture changed!");
        }
        else
        {
            Debug
                .Log("Captured Image not found! Material's texture NOT changed!");
        }
    }
}
