using System;
using System.Collections;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.Networking;

public class EmailSystem : MonoBehaviour
{
    private const string baseURL = "https://australia-southeast1-funclib-ddaaa.cloudfunctions.net/";



    private const string mailerEndPoint = "/mailerApp/";

    private string imageMailerEndPoint = "/mailerApp/jpegMailer/";


    public static EmailSystem Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }



    [Header("Email Variables")]

    [SerializeField] string sender = "noreply@t20biggesthitnsw.com.au";
    [SerializeField] string password = "79J2YbaCPBDNNmbnIas0";
    [SerializeField] string subject = "Example Subject";
    [SerializeField] string bucketName = "tla--cwc-vr-update";
    [SerializeField] string htmlFilePath = "Biggest-Hit--Email.html";


    private EmailContents GenerateEmailContents(string userEmail)
    {
        EmailContents emailContents = new EmailContents();
        emailContents.sender = sender;
        emailContents.smtpHost = "smtp.mail.us-west-2.awsapps.com";
        emailContents.smtpPort = 465;
        emailContents.secure = true;
        emailContents.senderUsername = sender;
        emailContents.senderPassword = password;
        emailContents.receiver = userEmail;
        emailContents.subject = subject;
        emailContents.htmlBucketName = bucketName;
        emailContents.htmlFilePath = htmlFilePath;

        return emailContents;
    }

    public void SendEmail(string userEmail, Action<bool, string> callback)
    {
        EmailContents emailContents = GenerateEmailContents(userEmail);

        StartCoroutine(POSTEmail(emailContents,
        baseURL + mailerEndPoint,
        (bool success, string message) =>
        {
            callback(success, message);
        }));
    }

    public void SendImageWithEmail(
        string userEmail,
        Texture2D image,
        string imageFormat,
        Action<bool, string> callback
    )
    {
        EmailContents emailContents = GenerateEmailContents(userEmail);
        emailContents.attachment = new Attachment();

        switch (imageFormat)
        {
            case "png":
                emailContents.attachment.attachmentData = image.EncodeToPNG();
                emailContents.attachment.attachmentName = "image.png";
                emailContents.attachment.mimeType = "image/png";
                break;
            case "jpeg":
                emailContents.attachment.attachmentData = image.EncodeToJPG();
                emailContents.attachment.attachmentName = "image.jpg";
                emailContents.attachment.mimeType = "image/jpg";
                break;
            case "jpg":
                emailContents.attachment.attachmentData = image.EncodeToJPG();
                emailContents.attachment.attachmentName = "image.jpg";
                emailContents.attachment.mimeType = "image/jpg";
                break;
            default:
                emailContents.attachment.attachmentData = image.EncodeToPNG();
                emailContents.attachment.attachmentName = "image.png";
                emailContents.attachment.mimeType = "image/png";
                break;
        }
        StartCoroutine(POSTEmail(emailContents,
        baseURL + imageMailerEndPoint,
        (bool success, string message) =>
        {
            callback(success, message);
        }));
    }


    private IEnumerator
    POSTEmail(
        EmailContents emailContents,
        string postURL,
        Action<bool, string> callback
    )
    {
        WWWForm form;
        EmailContentsToForm(emailContents, out form);
        UnityEngine.Debug.Log(form);

        //  Create web request pointing to the API url
        using (UnityWebRequest request = UnityWebRequest.Post(postURL, form))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            //  Handle the response
            switch (request.responseCode)
            {
                case 201: // Success!
                    callback(true, request.downloadHandler.text);
                    break;
                default: // Request failed
                    callback(false,
                    $"{request.responseCode} : {request.downloadHandler.text}");
                    break;
            }
        }
    }


    private void EmailContentsToForm(
        EmailContents emailContents,
        out WWWForm form
    )
    {
        form = new WWWForm();
        if (emailContents.attachment != null)
        {
            // UnityEngine.Debug.Log($"attachmentName: {emailContents.attachment.attachmentName}");
            // UnityEngine.Debug.Log($"mimeType: {emailContents.attachment.mimeType}");
            // UnityEngine.Debug.Log($"attachmentData: {emailContents.attachment.attachmentData}");
            form
                .AddBinaryData("attachment",
                emailContents.attachment.attachmentData,
                emailContents.attachment.attachmentName,
                emailContents.attachment.mimeType);
        }

        form.AddField("sender", emailContents.sender);
        form.AddField("smtpHost", emailContents.smtpHost);
        form.AddField("smtpPort", emailContents.smtpPort);
        form.AddField("secure", emailContents.secure ? "true" : "false");
        form.AddField("senderUsername", emailContents.senderUsername);
        form.AddField("senderPassword", emailContents.senderPassword);
        form.AddField("receiver", emailContents.receiver);
        form.AddField("subject", emailContents.subject);
        form.AddField("htmlBucketName", emailContents.htmlBucketName);
        form.AddField("htmlFilePath", emailContents.htmlFilePath);
    }


    public class EmailContents
    {
        public string sender;

        public string smtpHost;

        public int smtpPort;

        public bool secure;

        public string senderUsername;

        public string senderPassword;

        public string receiver;

        public string subject;

        public string htmlBucketName;

        public string htmlFilePath;

        public Attachment attachment;
    }

    public class Attachment
    {
        public Byte[] attachmentData;

        public string attachmentName;

        public string mimeType;
    }
}
