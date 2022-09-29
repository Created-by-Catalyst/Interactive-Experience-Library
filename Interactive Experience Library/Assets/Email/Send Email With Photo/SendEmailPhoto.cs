using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEmailPhoto : MonoBehaviour
{
    [Header("Reciever Email")]

    [SerializeField] string reciever;

    [SerializeField] PhotoCapture photoCapture;

    public string ReceiverEmail
    {
        get
        {
            return reciever;
        }
        set
        {
            reciever = value;
        }
    }

    public virtual void SendEmailToAddress(string emailAddress)
    {
        print(emailAddress);

        EmailSystem.Instance.SendImageWithEmail(emailAddress, photoCapture.image, "jpg", (bool success, string message) =>
        {
            Debug.Log($"{success} : {message}");
        });
    }


    [ContextMenu("Send Example Email")]
    public virtual void SendEmailToAddress()
    {
        SendEmailToAddress(reciever);
    }
}
