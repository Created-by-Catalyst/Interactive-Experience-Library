using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class KeyboardController : MonoBehaviour
{
    public static KeyboardController Instance { get; private set; }

    [SerializeField] bool dontDestroyOnLoad;
    [Space]
    [SerializeField] TMP_InputField inputField;
    [Space]
    [SerializeField] Image[] shift_keys;
    [SerializeField] Image[] capslockKeys;
    [SerializeField] Color shift_normal;
    [SerializeField] Color shift_selected;
    [Space]
    [SerializeField] Transform sendButton_T;
    [SerializeField] private GameObject[] pointers;

    bool isShift = false;
    bool isCapitalized = false;


    public delegate void Capitalize(bool isCapital);
    public event Capitalize capitalize;

    private TMP_InputField firstInput;

    // Start is called before the first frame update
    void Start()
    {
        firstInput = inputField;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddToInput(string mainInput, string altInput = "")
    {
        int fucklol;
        if (inputField.contentType == TMP_InputField.ContentType.Alphanumeric && !int.TryParse(mainInput, out fucklol))
        {
            return;
        }
        if (isShift)
        {
            inputField.text += altInput;
            Shift();
        }

        else
        {
            if (inputField.contentType == TMP_InputField.ContentType.Name && (inputField.text == "" || inputField.text[inputField.text.Length - 1] == ' '))
            {
                inputField.text += mainInput.ToUpper();
            }
            else
            {
                inputField.text += mainInput;
            }

        }
    }

    public void Space()
    {
        //inputField.text += " ";
        if (inputField.text != "")
        {
            AddToInput(" ", " ");
        }
    }

    public void Backspace()
    {
        char[] full = inputField.text.ToCharArray();

        if (full.Length < 1) { return; }

        string shortened = "";

        for (int i = 0; i < (full.Length - 1); i++)
        {
            shortened += full[i];
        }

        inputField.text = shortened;

        //CheckIfCurrentInputIsValid();
    }

    public void Shift()
    {
        isShift = !isShift;
        capitalize(isShift);
        Color c = isShift ? shift_selected : shift_normal;
        for (int i = 0; i < shift_keys.Length; i++)
        {
            shift_keys[i].color = c;
        }
    }

    public void ToggleCapitalize()
    {
        isCapitalized = !isCapitalized;

        Color c = isCapitalized ? shift_selected : shift_normal;
        for (int i = 0; i < capslockKeys.Length; i++)
        {
            capslockKeys[i].color = c;
        }

        capitalize(isCapitalized);
    }

    public void ResetPointer()
    {
        SetInputField(firstInput);
        SetPointer(pointers[0]);
    }

    


    public void CheckIfCurrentInputIsValid()
    {
        if (IsEmailValid(inputField.text))
        {
            sendButton_T.GetComponent<Button>().interactable = true;
            //sendButton_T.Find("Label").GetComponent<TextMeshProUGUI>().color = sendText_activated;
        }
        else
        {
            sendButton_T.GetComponent<Button>().interactable = false;
            //sendButton_T.Find("Label").GetComponent<TextMeshProUGUI>().color = sendText_deactivated;
        }
    }

    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    public bool IsEmailValid(string email)
    {
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }


    public void PrintTextToConsole()
    {
        print(inputField.text);
    }


    public void SetInputField(TMP_InputField newInputField)
    {
        inputField = newInputField;
    }
    public void SetPointer(GameObject pointer)
    {
        foreach (GameObject p in pointers)
        {
            p.SetActive(false);
        }
        pointer.SetActive(true);
    }
}
