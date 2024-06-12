using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LimitInput : MonoBehaviour
{
    public TMP_InputField mainInputField;

    void Start()
    {
        // Set the character limit in the main input field
        mainInputField.characterLimit = 5;

        // Set the validation method
        mainInputField.onValidateInput += delegate(string input, int charIndex, char addedChar)
        {
            return ValidateChar(addedChar);
        };
    }

    private char ValidateChar(char addedChar)
    {
        if (!char.IsLetter(addedChar))
        {
            // If the character is not a letter, don't add it to the input field
            addedChar = '\0';
        }
        return addedChar;
    }
}


