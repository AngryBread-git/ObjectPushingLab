using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFileFormatter : MonoBehaviour
{
    private string[] _textLines;

    public int NumberOfLinesInTextFile(TextAsset textFile) 
    {
        return textFile.text.Split("\n").Length;
    }

    public List<List<FormattedContent>> FormatTextFile(TextAsset textFile) 
    {
        if (textFile != null)
        {
            _textLines = textFile.text.Split('\n');
        }

        //remove carridge return.

        List<List<FormattedContent>> result = new List<List<FormattedContent>>();

        for (int i = 0; i < _textLines.Length; i++)
        {
            result.Add(FormatLineAtTags(_textLines[i]));
        }

        return result;

    }
    
    private List<FormattedContent> FormatLineAtTags(string line)
    {
        List<FormattedContent> result = new List<FormattedContent>();

        string tempContent = "";

        for (int i = 0; i < line.Length; i++)
        {
            char tempChar = line[i];

            //<> for tags. {} for events.
            //note: an event can not be inside a tag, or vice versa.
            if (tempChar == '<')
            {
                //Debug.Log(string.Format("Finished formatting text, text is: {0}", tempContent));

                result.Add(new FormattedText(tempContent));
                tempContent = "<";
            }
            else if (tempChar == '>')
            {
                //finish tag
                tempContent += tempChar;
                //Debug.Log(string.Format("Finished formatting tag, tag is: {0}", tempContent));

                result.Add(new FormattedTag(tempContent));
                tempContent = "";
            }

            else if (tempChar == '{')
            {
                //Debug.Log(string.Format("Finished formatting text, text is: {0}", tempContent));

                result.Add(new FormattedText(tempContent));
                //The '{' is not part of the EventInfo and as such is not saved.
                tempContent = "";
            }

            else if (tempChar == '}')
            {
                //finish event
                //The '}' is not part of the EventInfo and as such is not saved.
                Debug.Log(string.Format("Finished formatting eventcall, eventcall is: {0}", tempContent));

                EventInfo tempEventInfo = FormatEventCall(tempContent);
                result.Add(new FormattedEvent(tempEventInfo));
                tempContent = "";
            }

            else 
            { 
                tempContent += tempChar; 
            }         

        }
        //Add the remaining string at end of line.
        //Debug.Log(string.Format("Done with line, remainder-string is: {0}", tempContent));
        result.Add(new FormattedText(tempContent));

        //Debug.Log(string.Format("Done with line, amount of FormattedStrings: {0}", result.Count));
        return result;

    }

    private EventInfo FormatEventCall(string eventInfoString) 
    {
        string[] splitString = eventInfoString.Split(',');

        switch (splitString[0])
        {
            case "IncreaseTypingDelay":
                IncreaseTypingDelayEventInfo itdResult = new IncreaseTypingDelayEventInfo();
                itdResult._speedIncrease = Convert.ToSingle(splitString[1]);
                return itdResult;

            case "DecreaseTypingDelay":
                DecreaseTypingDelayEventInfo dtdResult = new DecreaseTypingDelayEventInfo();
                dtdResult._speedDecrease = Convert.ToSingle(splitString[1]);
                return dtdResult;

            case "PauseTyping":
                PauseTypingEventInfo ptResult = new PauseTypingEventInfo();
                ptResult._pauseDuration = Convert.ToSingle(splitString[1]);
                return ptResult;

            case "SetLineNr":
                SetLineNumberEventInfo slnResult = new SetLineNumberEventInfo();
                slnResult._lineNumber = Convert.ToInt32(splitString[1]);
                return slnResult;

            case "AutoNextLine":
                AutoNextLineEventInfo anlResult = new AutoNextLineEventInfo();
                anlResult._isAutoNextLine = bool.Parse(splitString[1]);
                return anlResult;
                
            case "ChangeMusic":
                ChangeMusicEventInfo cmResult = new ChangeMusicEventInfo();
                cmResult._audioClipNr = Convert.ToInt32(splitString[1]);
                return cmResult;

            case "SetTextAnimation":
                SetTextAnimationStyleEventInfo stasResult = new SetTextAnimationStyleEventInfo();
                stasResult._animationStyle = (AnimationStyle) Enum.Parse(typeof(AnimationStyle), splitString[1]);
                return stasResult;

            case "SetSpecifiedWordAnimation":
                SetSpecifiedWordAnimationEventInfo sswaResult = new SetSpecifiedWordAnimationEventInfo();
                sswaResult._animatedOnlyOneWord = bool.Parse(splitString[1]);

                Debug.Log(string.Format("splitString is: {0}", splitString.Length));

                //The first two values in split string are a string and bool respectivly.
                //The ints are values 2 and after.
                for (int i = 2; i < splitString.Length; i++) 
                {
                    Debug.Log(string.Format("splitString[i] is: {0}", splitString[i]));

                    sswaResult._specifiedWordIndexes.Add(Convert.ToInt32(splitString[i]));
                }

                return sswaResult;

            case "SetTextShake":
                SetTextShakeEventInfo stsResult = new SetTextShakeEventInfo();
                stsResult._shakeHeightSpeed = Convert.ToSingle(splitString[1]);
                stsResult._shakeWidthSpeed = Convert.ToSingle(splitString[2]);
                return stsResult;

            case "SetTextWobble":
                SetTextWobbleEventInfo stwResult = new SetTextWobbleEventInfo();
                stwResult._wobbleHeightSpeed = Convert.ToSingle(splitString[1]);
                stwResult._wobbleWidthSpeed = Convert.ToSingle(splitString[2]);
                return stwResult;

            case "SetTextFloat":
                SetTextFloatEventInfo stfResult = new SetTextFloatEventInfo();
                stfResult._floatHeightSpeed = Convert.ToSingle(splitString[1]);
                stfResult._floatWidthSpeed = Convert.ToSingle(splitString[2]);
                return stfResult;

            case "SetTextWave":
                SetTextWaveEventInfo stwaResult = new SetTextWaveEventInfo();
                stwaResult._waveSpeed = Convert.ToSingle(splitString[1]);
                stwaResult._waveLength = Convert.ToSingle(splitString[2]);
                stwaResult._waveHeight = Convert.ToSingle(splitString[3]);
                return stwaResult;

            case "SetNextStage":
                NextStageEvent snsResult = new NextStageEvent();
                snsResult._nextStageNr = Convert.ToInt32(splitString[1]);
                return snsResult;

            case "AddUIElement":
                AddUIElementEvent aueResult = new AddUIElementEvent();
                aueResult._UIElementNr = Convert.ToInt32(splitString[1]);
                return aueResult;
            case "AddHat":
                AddHatEvent ahResult = new AddHatEvent();
                ahResult._hatNr = Convert.ToInt32(splitString[1]);
                return ahResult;

            default:
                Debug.LogWarning(string.Format("Default in FormatEventCall. EventType {0} does not exist.", splitString[0]));
                DebugEventInfo debugResult = new DebugEventInfo();
                debugResult.EventDescription = ("Default in FormatEventCall. EventType does not exist.");
                debugResult.PriorityLevel = 1;
                return debugResult;
        }

    }

}
