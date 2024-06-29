using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class IncreaseTypingDelayEventInfo : EventInfo
{
    //written as "{IncreaseTypingDelay,X}". in the textfile. With X being a valid float.
    public float _speedIncrease;
}


public class DecreaseTypingDelayEventInfo : EventInfo
{
    //written as "{DecreaseTypingDelay,X}". in the textfile. With X being a valid float.
    //Note: X is a positive value, as it is subtracted from the delay.
    public float _speedDecrease;
}

public class PauseTypingEventInfo : EventInfo
{
    //written as "{PauseTyping,X}". in the textfile. With X being a valid float.
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

public class SetSpecifiedWordAnimationEventInfo : EventInfo
{
    //written as "{SetSpecifiedWordAnimation,X,Y,...}". in the textfile. With X being a bool. Y being an int, followed by any amount of ints.
    //Note: See the text animator for the AnimaitonsStyles
    //Note: The words are zero-indexed. And seperated by spaces.
    public bool _animatedOnlyOneWord;
    public List<int> _specifiedWordIndexes = new List<int>();
}


//set shake, set wobble, set float, set wave.
public class SetTextShakeEventInfo : EventInfo
{
    //written as "{SetTextShake,X,Y}". in the textfile. With X (height) and Y (width) being floats.
    public float _shakeHeightSpeed;
    public float _shakeWidthSpeed;
}

public class SetTextWobbleEventInfo : EventInfo
{
    //written as "{SetTextWobble,X,Y}". in the textfile. With X (height) and Y Y (width) being floats.
    public float _wobbleHeightSpeed;
    public float _wobbleWidthSpeed;
}

public class SetTextFloatEventInfo : EventInfo
{
    //written as "{SetTextFloat,X,Y}". in the textfile. With X (height) and Y Y (width) being floats.
    public float _floatHeightSpeed;
    public float _floatWidthSpeed;
}


public class SetTextWaveEventInfo : EventInfo
{
    //written as "{SetTextWave,X,Y,Z}". in the textfile. With X (speed), Y (length) and Z (height) being floats.
    public float _waveSpeed;
    public float _waveLength;
    public float _waveHeight;
}