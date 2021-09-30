using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;

public class WordDictionaryManager : MonoBehaviour
{
    private Timer timer;


    [Header("Selected Dictionary")]
    public Text currentLetter;
    private string[] storedDictionnary;
    private bool dictionnaryLoaded;


    [Header("Setup Texts")]
    public InputField inputField;
    public InputField currentInputFieldText;

    public Text wordLevelText;

    [Header("Text to display")]
    public Text previousPreviousText;
    public Text previousText;

    public Text currentText;
    [HideInInspector]public int currentLineNumber;

    public Text nextText;
    public Text nextNextText;

    [Header("Go to line")]
    public InputField goToLineInput;

    private double time;
    private bool isSame = false;
    private bool hasUpdated;

    [HideInInspector] public int seconds;
    [HideInInspector] public int minutes;
    [HideInInspector] public int hours;
    [HideInInspector] public int nonResetedSeconds;


    public void SavePlayer() => SaveSystem.SavePlayer(this);

    private void Start()
    {
        dictionnaryLoaded = false;
        timer = GetComponent<Timer>();
        inputField.ActivateInputField();
    }

    static async Task<string[]> ReadTextFileInBackgroundThread(string filePath)
    {
        //Load large file on different thread to prevent a freeze
        return await Task.Run(() => { return File.ReadAllLines(filePath); });
    }

    async public void StartGame(string selectedDictionnaryPath)
    {
        storedDictionnary = await ReadTextFileInBackgroundThread(selectedDictionnaryPath);
        dictionnaryLoaded = true;
    }

    private void Update()
    {
        if (!dictionnaryLoaded) return;

        SavePlayer();

        CheckTextInput();
        UpdateWordLevelUI();
        UpdateCurrentLetterUI();

        seconds = timer.seconds;
        minutes = timer.minutes;
        hours = timer.hours;
        nonResetedSeconds = timer.nonResetedSeconds;

        time = timer.nonResetedSeconds;

        //--------------------------[     All rendered texts     ]-------------------------------------

        if(currentLineNumber > 1)
            previousPreviousText.text = storedDictionnary[currentLineNumber - 2];
        else
            previousPreviousText.text = "";
        if (currentLineNumber > 0)
            previousText.text = storedDictionnary[currentLineNumber - 1];
        else
            previousText.text = "";

        currentText.text = storedDictionnary[currentLineNumber];

        if (currentLineNumber == storedDictionnary.Length - 1)
            nextText.text = "";
        else
            nextText.text = storedDictionnary[currentLineNumber + 1];

        if (currentLineNumber == storedDictionnary.Length - 2)
            nextNextText.text = "";
        else
            nextNextText.text = storedDictionnary[currentLineNumber + 2];

        //------------------------------------------------------------------------------------------------

        //Check
        if (isSame && !hasUpdated)
        {
            currentInputFieldText.GetComponent<Image>().color = Color.green;

            currentLineNumber++;

            BlankField(inputField);

            hasUpdated = true;
        }
        else
        {
            hasUpdated = false;
            currentInputFieldText.GetComponent<Image>().color = Color.white;
        }
    }

    private void UpdateCurrentLetterUI()
    {
        //Format the first letter of the current word to uppercase
        currentLetter.text = currentText.text.Substring(0, 1).ToUpper();
    }

    private void CheckTextInput()
    {
        if(inputField.text.Trim() == currentText.text.Trim().ToLower())
            isSame = true;
        else
            isSame = false;
    }

    private void BlankField(InputField input)
    {
        input.Select();
        input.text = "";
    }

    private void UpdateWordLevelUI()
    {
        wordLevelText.text = $"{currentLineNumber } / {storedDictionnary.Length}";
    }

    public void OnClick_SetCurrentWord()
    {
        int value = 0;

        if (goToLineInput.text != "")
        {
           value = int.Parse(goToLineInput.text);
        }


        if (value > storedDictionnary.Length) return;

        currentLineNumber = value - 1;
        goToLineInput.text = "";
    }
}
