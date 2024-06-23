using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextFileFormatter))]
public class DialogueSystemV2 : MonoBehaviour
{
    //Other Classes
    [SerializeField] private TextFileFormatter _textFileFormatter;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject _textGameObject;
    [SerializeField] private GameObject _textBox;
    [SerializeField] private GameObject _interactIndicator;
    //[SerializeField] private TMP_Animation _tmpAnimation;

    //Setttings
    [SerializeField] private List<TextAsset> _mainTextFiles;
    [SerializeField] [Range(0.01f, 0.5f)] private float _typingDelay;
    private float _orgTypingDelay;

    private List<List<FormattedContent>> _formattedLines;

    //Status values
    [SerializeField] private int _currentTextFileNr;
    private bool _isDialogueActive;
    private bool _isTyping;
    private bool _isPaused;
    private float _pauseDuration;
    private bool _autoPrintNextLine;
    public bool _advanceTextInput;

    [SerializeField] private int _currentTextFileLineAmount;
    [SerializeField] private int _currentLineNr = 0;


    void Start()
    {
        //Debug.Log(string.Format("Formated Debug at start of DialogueSystemV1"));
        _textFileFormatter = GetComponent<TextFileFormatter>();


        CheckTypingDelay();

        _orgTypingDelay = _typingDelay;
        _textMeshPro.text = "";

        if (_textBox != null)
        {
            _textBox.SetActive(false);
        }
        _textGameObject.SetActive(false);

    }

    private void OnEnable()
    {
        EventCoordinator<IncreaseTypingDelayEventInfo>.RegisterListener(IncreaseTypingDelay);
        EventCoordinator<DecreaseTypingDelayEventInfo>.RegisterListener(DecreaseTypingDelay);
        EventCoordinator<PauseTypingEventInfo>.RegisterListener(PauseTyping);
        EventCoordinator<SetLineNumberEventInfo>.RegisterListener(SetCurrentLineNumber);
        EventCoordinator<AutoNextLineEventInfo>.RegisterListener(SetAutoNextLine);

    }

    private void OnDisable()
    {
        EventCoordinator<IncreaseTypingDelayEventInfo>.UnregisterListener(IncreaseTypingDelay);
        EventCoordinator<DecreaseTypingDelayEventInfo>.UnregisterListener(DecreaseTypingDelay);
        EventCoordinator<PauseTypingEventInfo>.UnregisterListener(PauseTyping);
        EventCoordinator<SetLineNumberEventInfo>.UnregisterListener(SetCurrentLineNumber);
        EventCoordinator<AutoNextLineEventInfo>.UnregisterListener(SetAutoNextLine);
    }

    private void Update()
    {
        /*if (!_isDialogueActive && Input.GetKeyDown(KeyCode.Q)) 
        {
            Debug.Log(string.Format("No dialogue active. input Q."));

            StartDialogue(0, 0);
        }
        */
        if (_isDialogueActive  && _advanceTextInput)
        {
            _advanceTextInput = false;
            Debug.Log(string.Format("Dialogue active. input E."));
            LoadNextLine();
        }
    }

    public void StartDialogue(int textFileToLoad, int lineToLoad, string textFileType) 
    {
        //set values
        _isDialogueActive = true;
        _currentTextFileNr = textFileToLoad;
        _currentLineNr = lineToLoad;


        if (_mainTextFiles.Count < _currentTextFileNr)
        {
            Debug.LogWarning(string.Format("Textfile nr greater than amount of textfiles for main."));
            _currentTextFileLineAmount = _textFileFormatter.NumberOfLinesInTextFile(_mainTextFiles[0]);
        }
        _currentTextFileLineAmount = _textFileFormatter.NumberOfLinesInTextFile(_mainTextFiles[_currentTextFileNr]);


        //activate assets.
        if (_textBox != null)
        {
            _textBox.SetActive(true);
        }

        _textGameObject.SetActive(true);
        _textMeshPro.text = "";

        _formattedLines = _textFileFormatter.FormatTextFile(_mainTextFiles[_currentTextFileNr]);
        StartCoroutine(PrintLine(_formattedLines[_currentLineNr]));

    }

    private void EndDialogue() 
    {
        Debug.Log(string.Format("EndDialogue"));
        _isDialogueActive = false;


        if (_textBox != null)
        {
            _textBox.SetActive(false);
        }

        _textGameObject.SetActive(false);
        _textMeshPro.text = "";

    }


    //move to next line.
    private void LoadNextLine() 
    {
        if (!_isTyping && _currentLineNr < _currentTextFileLineAmount - 1)
        {
            
            _textMeshPro.text = "";

            _currentLineNr += 1;
     
            StartCoroutine(PrintLine(_formattedLines[_currentLineNr]));
        }
        else if (!_isTyping && _currentLineNr >= _currentTextFileLineAmount - 1) 
        {
            EndDialogue();
        }
    }

    private void SetIsTyping(bool givenValue) 
    {
        if (givenValue)
        {
            _isTyping = true;
            if (_interactIndicator != null) 
            {
                _interactIndicator.SetActive(false);
            }
            

        }
        else 
        {
            _isTyping = false;
            if (_interactIndicator != null)
            {
                _interactIndicator.SetActive(true);
            }
        }
    }

    private void SetCurrentLineNumber(SetLineNumberEventInfo ei) 
    {
        if (ei._lineNumber >= _currentTextFileLineAmount - 1)
        {
            Debug.LogWarning(string.Format("SetCurrentLineNumber, ei._lineNumber: {0} was greater than _currentTextFileLineAmount: {1}. _currentLineNR will not be changed", ei._lineNumber, _currentTextFileLineAmount));

        }
        else 
        {
            //_currentLineNr is increased by 1 in load next line. In order to load Line X then _currentLineNr needs to be X - 1.
            _currentLineNr = ei._lineNumber - 1;
            Debug.Log(string.Format("SetCurrentLineNumber, _currentLineNr is now: {0}", _currentLineNr));
            Debug.Log(string.Format("SetCurrentLineNumber, next line will be: {0}", _currentLineNr + 1));
        }
        
    }

    private void SetAutoNextLine(AutoNextLineEventInfo ei) 
    {
        _autoPrintNextLine = ei._isAutoNextLine;
    }

    //Typing timing methods.
    private void IncreaseTypingDelay(IncreaseTypingDelayEventInfo ei) 
    {
        _typingDelay += ei._speedIncrease;

        Debug.Log(string.Format("IncreaseTypingDelay, _speedDecrease is: {0}", ei._speedIncrease));
        Debug.Log(string.Format("IncreaseTypingDelay, new _typingDelay is: {0}", _typingDelay));

        CheckTypingDelay();
    }

    private void DecreaseTypingDelay(DecreaseTypingDelayEventInfo ei)
    {
        _typingDelay -= ei._speedDecrease;
        Debug.Log(string.Format("DecreaseTypingDelay, _speedDecrease is: {0}", ei._speedDecrease));
        Debug.Log(string.Format("DecreaseTypingDelay, new _typingDelay is: {0}", _typingDelay));

        CheckTypingDelay();
    }

    private void CheckTypingDelay() 
    {
        if (_typingDelay < 0.01f)
        {
            Debug.Log(string.Format("CheckTypingDelay. givenTypingDelay was below 0.01. Setting it to 0.01"));
            _typingDelay = 0.01f;
        }
        else if (_typingDelay > 0.5f)
        {
            Debug.Log(string.Format("CheckTypingDelay. givenTypingDelay was above 0.5. Setting it to 0.5"));
            _typingDelay = 0.05f;
        }
    }

    private void PauseTyping(PauseTypingEventInfo ei) 
    {
        
        if (ei._pauseDuration > 0)
        {
            Debug.Log(string.Format("PauseTyping, ei_pauseDuration is: {0}", ei._pauseDuration));
            _pauseDuration = ei._pauseDuration;

        }
        else 
        {
            Debug.Log(string.Format("PauseTyping, ei_pauseDuration was below 0: {0}", ei._pauseDuration));
            _pauseDuration = 1.0f;
        }

        _isPaused = true;
    }

    private IEnumerator PrintLine(List<FormattedContent> currentLine) 
    {
        SetIsTyping(true);

        //first FormattedString
        for (int i = 0; i < currentLine.Count; i++) 
        {
           
            //if it's a tag, type it out all at once to apply it.
            if (currentLine[i].ContentType == ContentType.tag)
            {
                FormattedTag tempTag = (FormattedTag)currentLine[i];
                _textMeshPro.text += tempTag.Tag;
            }
            //if it's an Event then fire it.
            else if (currentLine[i].ContentType == ContentType.eventinfo)
            {
                FormattedEvent tempEvent = (FormattedEvent)currentLine[i];
                FireFormattedEvent(tempEvent);

                EventInfo eventInfo = tempEvent.Info;
                //Type type = (Type)tempEvent._eventType;

            }
            //else if it's a string then type it out a character at a time.
            else if (currentLine[i].ContentType == ContentType.text)
            {

                FormattedText tempText = (FormattedText)currentLine[i];

                string lineSubsection = tempText.Text;

                for (int j = 0; j < lineSubsection.Length; j++)
                {
                    _textMeshPro.text += lineSubsection[j];

                    //_tmpAnimation.AddCharacter(lineSubsection[j]);

                    yield return new WaitForSeconds(_typingDelay);

                    /*if (lineSubsection[j].Equals(' '))
                    {
                        _tmpAnimation.ReadTextMesh();
                    }*/
                }
                
            }
            
            //if a PauseEvent is fired then this waits for the duration then continues.
            if (_isPaused) 
            {
                yield return new WaitForSeconds(_pauseDuration);
                _pauseDuration = 0.0f;
                _isPaused = false;
            }

        }
        //_currentLineNr += 1;
        SetIsTyping(false);
        //_tmpAnimation.ReadTextMesh();

        if (_autoPrintNextLine) 
        {
            LoadNextLine();
        }
    }

    //Needed due to Types not being dynamic, the calls have to be explicit
    //So the switch case is needed to make explicit calls.
    private void FireFormattedEvent(FormattedEvent tempEvent) 
    {
        switch (tempEvent.Info) 
        {
            case IncreaseTypingDelayEventInfo ei:
                EventCoordinator<IncreaseTypingDelayEventInfo>.FireEvent(ei);
                break;
            case DecreaseTypingDelayEventInfo ei:
                EventCoordinator<DecreaseTypingDelayEventInfo>.FireEvent(ei);
                break;
            case PauseTypingEventInfo ei:
                EventCoordinator<PauseTypingEventInfo>.FireEvent(ei);
                break;
            case SetLineNumberEventInfo ei:
                EventCoordinator<SetLineNumberEventInfo>.FireEvent(ei);
                break;
            case AutoNextLineEventInfo ei:
                EventCoordinator<AutoNextLineEventInfo>.FireEvent(ei);
                break;
            case ChangeMusicEventInfo ei:
                EventCoordinator<ChangeMusicEventInfo>.FireEvent(ei);
                break;
            case SetTextAnimationStyleEventInfo ei:
                EventCoordinator<SetTextAnimationStyleEventInfo>.FireEvent(ei);
                break;
            case SetSpecifiedWordAnimationEventInfo ei:
                EventCoordinator<SetSpecifiedWordAnimationEventInfo>.FireEvent(ei);
                break;
            case SetTextShakeEventInfo ei:
                EventCoordinator<SetTextShakeEventInfo>.FireEvent(ei);
                break;
            case SetTextWobbleEventInfo ei:
                EventCoordinator<SetTextWobbleEventInfo>.FireEvent(ei);
                break;
            case SetTextFloatEventInfo ei:
                EventCoordinator<SetTextFloatEventInfo>.FireEvent(ei);
                break;
            case SetTextWaveEventInfo ei:
                EventCoordinator<SetTextWaveEventInfo>.FireEvent(ei);
                break;

            case NextStageEvent ei:
                EventCoordinator<NextStageEvent>.FireEvent(ei);
                break;

            case AddUIElementEvent ei:
                EventCoordinator<AddUIElementEvent>.FireEvent(ei);
                break;
            case AddHatEvent ei:
                EventCoordinator<AddHatEvent>.FireEvent(ei);
                break;

            case DebugEventInfo ei:
                Debug.LogWarning(string.Format("DebugEvent: {0}", ei.EventDescription));
                break;
            case null:
                Debug.LogWarning(string.Format("Not a valid event."));
                break;
        }
    }

}
