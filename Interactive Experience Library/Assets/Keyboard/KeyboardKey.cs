using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardKey : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI main;
    [SerializeField] TextMeshProUGUI alt;
    [Space]
    [SerializeField] bool mainIsLetter;
    KeyboardController k_ctrl;

    private void OnEnable()
    {
        StartCoroutine(SubscribeToCapitalize());
    }

    IEnumerator SubscribeToCapitalize()
    {
        while (k_ctrl == null)
        {
            k_ctrl = KeyboardController.Instance;
            yield return null;
        }
        
        k_ctrl.capitalize += Capitalize;
    }


    private void OnDisable()
    {
        if (k_ctrl == null)
        {
            k_ctrl = KeyboardController.Instance;
        }
        if (k_ctrl != null)
            k_ctrl.capitalize -= Capitalize;
    }

    // Start is called before the first frame update
    void Start()
    {
        k_ctrl = KeyboardController.Instance;
    }

    public void KeyPressed()
    {
        k_ctrl.AddToInput(main.text, alt.text);
    }


    void Capitalize(bool isCapital)
    {
        if (mainIsLetter)
        {

            //Debug.Log("Trying to capitalise " + gameObject.name);
            main.text = isCapital ? main.text.ToUpper() : main.text.ToLower();
        }
    }
}
