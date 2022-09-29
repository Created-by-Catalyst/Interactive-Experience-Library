using UnityEngine;

public class SendEmail : MonoBehaviour
{

    [Header("Reciever Email")]

    [SerializeField] string reciever;


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

        EmailSystem.Instance.SendEmail(emailAddress, (bool success, string message) =>
            {
                Debug.Log($"{success} : {message}");
            });
    }


    [ContextMenu("Send Example Email")]
    public virtual void SendEmailToAddress()
    {
        SendEmailToAddress (reciever);
    }
}
