using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputWindow : MonoBehaviour
{
    private Text titleText;
    private TMP_InputField inputField;
    private Action Submit;

    private void Awake()
    {
        titleText = transform.Find("titleText").GetComponent<Text>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();

        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Submit();
        }
    }

    public void Show(string titleString, string inputString, string validCharacters, int characterLimit, Action<string> onEnter)
    {
        gameObject.SetActive(true);

        titleText.text = titleString;

        inputField.characterLimit = characterLimit;
        inputField.onValidateInput = (string text, int charIndex, char addedChar) => {
            return ValidateChar(validCharacters, addedChar);
        };

        Submit = () => {
            Hide();
            onEnter(inputField.text);
        };
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private char ValidateChar(string validCharacters, char addedChar)
    {
        if (validCharacters.IndexOf(addedChar) != -1)
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }
}
