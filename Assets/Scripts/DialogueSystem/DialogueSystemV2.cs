using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum TypingDelaySetting
{
    fast,
    normal,
    slow,
}


public class DialogueSystemV2 : MonoBehaviour
{
    //Other Classes
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject _textGameObject;
    [SerializeField] private GameObject _textBox;
    [SerializeField] private GameObject _interactIndicator;
    //[SerializeField] private TMP_Animation _tmpAnimation;

    //Settings
    [SerializeField] private TypingDelaySetting _typingDelaySetting = TypingDelaySetting.normal;
    [SerializeField] [Range(0.01f, 0.3f)] private float _fastTypingDelay;
    [SerializeField] [Range(0.01f, 0.3f)] private float _normalTypingDelay;
    [SerializeField] [Range(0.01f, 0.3f)] private float _slowTypingDelay;
    private float _currentTypingDelay;

    private List<List<FormattedContent>> _formattedLines;

    //Status values
    [SerializeField] private int _currentTextFileNr;
    private bool _isDialogueActive;
    private bool _isTyping;
    private bool _isPaused;
    private float _pauseDuration;
    private bool _autoPrintNextLine;
    public bool _advanceTextInput;

    [SerializeField] private int _lineAmount;
    [SerializeField] private int _currentLineNr = 0;

    public bool GetIsDialogueActive() 
    {
        return _isDialogueActive;
    }

    void Start()
    {
        ApplyTypingDelaySetting();

        _textMeshPro.text = "";

        if (_textBox != null)
        {
            _textBox.SetActive(false);
        }
        _textGameObject.SetActive(false);
        _interactIndicator.SetActive(false);

    }

    private void OnEnable()
    {
        EventCoordinator<SetTypingDelayEventInfo>.RegisterListener(SetTypingDelaySetting);

        EventCoordinator<PauseTypingEventInfo>.RegisterListener(PauseTyping);
        EventCoordinator<SetLineNumberEventInfo>.RegisterListener(SetCurrentLineNumber);
        EventCoordinator<AutoNextLineEventInfo>.RegisterListener(SetAutoNextLine);

    }

    private void OnDisable()
    {
        EventCoordinator<SetTypingDelayEventInfo>.UnregisterListener(SetTypingDelaySetting);

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

    public void StartDialogue(int startLine, int lineAmount, List<List<FormattedContent>> formattedLines) 
    {
        //If there's a dialogue active, then dont start a new one.
        if (_isDialogueActive) 
        {
            return;
        }

        //set values
        _isDialogueActive = true;

        _currentLineNr = startLine;
        _lineAmount = lineAmount;
        _formattedLines = formattedLines;


        //activate assets.
        if (_textBox != null)
        {
            _textBox.SetActive(true);
        }

        _textGameObject.SetActive(true);
        _interactIndicator.SetActive(true);
        _textMeshPro.text = "";
        _textMeshPro.maxVisibleCharacters = 0;

        StartCoroutine(PrintLine(_formattedLines[_currentLineNr]));

    }

    private void EndDialogue() 
    {
        Debug.Log(string.Format("EndDialogue"));
        _isDialogueActive = false;
        _autoPrintNextLine = false;

        SetTextAnimationStyleEventInfo ei = new SetTextAnimationStyleEventInfo();
        ei._animationStyle = (AnimationStyle)Enum.Parse(typeof(AnimationStyle), "none");
        EventCoordinator<SetTextAnimationStyleEventInfo>.FireEvent(ei);

        if (_textBox != null)
        {
            _textBox.SetActive(false);
        }

        _textGameObject.SetActive(false);
        _interactIndicator.SetActive(false);
        _textMeshPro.text = "";
        _textMeshPro.maxVisibleCharacters = 0;


    }


    //move to next line.
    private void LoadNextLine() 
    {
        if (!_isTyping && _currentLineNr < _lineAmount - 1)
        {
            
            _textMeshPro.text = "";
            _textMeshPro.maxVisibleCharacters = 0;

            _currentLineNr += 1;
     
            StartCoroutine(PrintLine(_formattedLines[_currentLineNr]));
        }
        else if (!_isTyping && _currentLineNr >= _lineAmount - 1) 
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
        if (ei._lineNumber >= _lineAmount - 1)
        {
            Debug.LogWarning(string.Format("SetCurrentLineNumber, ei._lineNumber: {0} was greater than _currentTextFileLineAmount: {1}. _currentLineNR will not be changed", ei._lineNumber, _lineAmount));

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

    private void SetTypingDelaySetting(SetTypingDelayEventInfo ei)
    {
        _typingDelaySetting = ei._typingDelaySetting;

        ApplyTypingDelaySetting();
    }

    private void ApplyTypingDelaySetting()
    {
        if (_typingDelaySetting == TypingDelaySetting.fast)
        {
            _currentTypingDelay = _fastTypingDelay;
        }
        else if (_typingDelaySetting == TypingDelaySetting.normal)
        {
            _currentTypingDelay = _normalTypingDelay;
        }
        else if (_typingDelaySetting == TypingDelaySetting.slow)
        {
            _currentTypingDelay = _slowTypingDelay;
        }
    }



    private void PauseTyping(PauseTypingEventInfo ei) 
    {
        
        if (ei._pauseDuration > 0)
        {
            //Debug.Log(string.Format("PauseTyping, ei_pauseDuration is: {0}", ei._pauseDuration));
            _pauseDuration = ei._pauseDuration;

        }
        else 
        {
            //Debug.Log(string.Format("PauseTyping, ei_pauseDuration was below 0: {0}", ei._pauseDuration));
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
                //add the text.
                _textMeshPro.text += lineSubsection;

                for (int j = 0; j < lineSubsection.Length; j++)
                {
                    //make the new text show up, one character at a time.
                    _textMeshPro.maxVisibleCharacters += 1;

                    //_tmpAnimation.AddCharacter(lineSubsection[j]);

                    if (!lineSubsection[j].Equals(' '))
                    {
                        PlayDialogueSoundEvent ei = new PlayDialogueSoundEvent();
                        EventCoordinator<PlayDialogueSoundEvent>.FireEvent(ei);
                    }

                    //If it's punctuation then the typing has a slightly longer delay.
                    if (lineSubsection[j].Equals('.') || lineSubsection[j].Equals(',') || lineSubsection[j].Equals('!') || lineSubsection[j].Equals('?'))
                    {
                        yield return new WaitForSeconds(_currentTypingDelay * 1.2f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(_currentTypingDelay);
                    }

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
            case SetTypingDelayEventInfo ei:
                EventCoordinator<SetTypingDelayEventInfo>.FireEvent(ei);
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
            case SetTextAnimationIntensityEventInfo ei:
                EventCoordinator<SetTextAnimationIntensityEventInfo>.FireEvent(ei);
                break;
            case SetSpecifiedWordAnimationEventInfo ei:
                EventCoordinator<SetSpecifiedWordAnimationEventInfo>.FireEvent(ei);
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
