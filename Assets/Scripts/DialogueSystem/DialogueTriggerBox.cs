using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBox : MonoBehaviour
{
    [SerializeField] private int _textFileToLoad;
    [SerializeField] private List<TextAsset> _textFiles;
    [SerializeField] private bool _allowPlayerToTrigger = false;


    private DialogueSystemV2 _dialogueSystem;
    private TextFileFormatter _textFileFormatter;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueSystem = FindObjectOfType<DialogueSystemV2>();
        _textFileFormatter = FindObjectOfType<TextFileFormatter>();
        //Debug.Log(string.Format("DialogueTrigger, start"));
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("DialogueTrigger, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder") && !_dialogueSystem.GetIsDialogueActive())
        {

            FormatDialogue();
        }
        else if (other.CompareTag("Player") && _allowPlayerToTrigger && !_dialogueSystem.GetIsDialogueActive()) 
        {
            FormatDialogue();
        }
    }

    private void FormatDialogue()
    {
        int lineAmount;
        List<List<FormattedContent>> formattedLines;

        if (_textFiles.Count <= _textFileToLoad)
        {
            Debug.LogWarning(string.Format("Textfile nr greater than amount of textfiles. Loading last textfile"));
            lineAmount = _textFileFormatter.NumberOfLinesInTextFile(_textFiles[_textFiles.Count - 1]);
            formattedLines = _textFileFormatter.FormatTextFile(_textFiles[_textFiles.Count - 1]);
        }
        else
        {
            lineAmount = _textFileFormatter.NumberOfLinesInTextFile(_textFiles[_textFileToLoad]);
            formattedLines = _textFileFormatter.FormatTextFile(_textFiles[_textFileToLoad]);
        }

        CallStartDialogue(lineAmount, formattedLines);
    }


    private void CallStartDialogue(int lineAmount, List<List<FormattedContent>> formattedLines)
    {
        
        _dialogueSystem.StartDialogue(0, lineAmount, formattedLines);
        _textFileToLoad += 1;
    }

}
