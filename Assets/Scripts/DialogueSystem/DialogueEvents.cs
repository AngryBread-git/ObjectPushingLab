using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SetTypingDelayEventInfo : EventInfo
{
    //written as "{SetTypingDelay,X}". in the textfile. With X being a valid TypingDelaySetting.
    //Note: See the DialogurSystem for the TypingDelaySettings
    public TypingDelaySetting _typingDelaySetting;
}

public class PauseTypingEventInfo : EventInfo
{
    //written as "{PauseTyping,X}". in the textfile. 
    //NB! With X being the delay in miliseconds. 
    //So {PauseTyping,2000} give a delay of 2 seconds
    //And {PauseTyping,500}give a delay of half a second
    public float _pauseDuration;
}

public class SetLineNumberEventInfo : EventInfo
{
    //written as "{SetLineNr,X}". in the textfile. With X being a valid int.
    //Note: the text file is read into an array, line 4 is in place 3 in the array.
    //So in order to go to line 4, the textfile should read "{SetLineNr,3}"
    public int _lineNumber;
}

public class AutoNextLineEventInfo : EventInfo
{
    //written as "{AutoNextLine,X}". in the textfile. With X being a bool value. true or false.
    //Makes it so the dialogue goes to the next line after the current line is complete. 
    //Well used with the PauseTyping event.
    public bool _isAutoNextLine;
}

public class ChangeMusicEventInfo : EventInfo
{
    //written as "{ChangeMusic}" in the textfile. 
    //Note: could have an AudioClip, but this is a simple example.
}

public class SetTextAnimationStyleEventInfo : EventInfo
{
    //written as "{SetTextAnimation,X}". in the textfile. With X being a AnimationStyle
    //Note: See the text animator for the AnimaitonsStyles
    public AnimationStyle _animationStyle;
}

public class SetTextAnimationIntensityEventInfo : EventInfo
{
    //written as "{SetTextAnimationIntensity,X}". in the textfile. With X being a TextAnimationIntensity
    //Note: See the text animator for the TextAnimationIntensitys and their values.
    public TextAnimationIntensity _textAnimationIntensity;
}

public class SetSpecifiedWordAnimationEventInfo : EventInfo
{
    //written as "{SetSpecifiedWordAnimation,X,Y,...}". in the textfile. With X being a bool. Y being an int, followed by any amount of ints.
    //Note: See the text animator for the AnimaitonsStyles
    //Note: The words are zero-indexed. And seperated by spaces.
    public bool _animatedOnlyOneWord;
    public List<int> _specifiedWordIndexes = new List<int>();
}

