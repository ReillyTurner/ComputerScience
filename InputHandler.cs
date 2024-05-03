using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class InputHandler : MonoBehaviour
{
    public GameObject game;
    public GameObject canvas;

    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;
    [SerializeField] Text resultTextGame;

    void Start()
    {
        game.SetActive(false);
    }

    public void ValidateInput()
    {
        string input = inputField.text;

        if (input.Length > 10)
        {
            resultText.text = "Input is too long.";
            resultText.color = Color.red;
            StartCoroutine(ClearResults());
        }

        else if (!Regex.IsMatch(input, @"^[a-zA-Z]+$"))
        {
            resultText.text = "Input must contain only letters.";
            resultText.color = Color.red;
            StartCoroutine(ClearResults());
        }
        else if (input.Length < 4)
        {
            resultText.text = "Input is too short.";
            resultText.color = Color.red;
            StartCoroutine(ClearResults());
        }

        else
        {
            resultText.text = "Input is valid.";
            resultText.color = Color.green;
            game.SetActive(true);
            resultTextGame.text = input;
            canvas.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ClearResults()
    {
        yield return new WaitForSeconds(2);
        resultText.text = "Submit";
        resultText.color = Color.black;
    }
}
