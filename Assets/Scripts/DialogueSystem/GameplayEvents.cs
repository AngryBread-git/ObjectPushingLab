using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageEvent : EventInfo
{
    //written as "{NextStage,X}". in the textfile. With X being a valid int.
    public int _nextStageNr;
}

public class AddUIElementEvent : EventInfo
{
    //written as "{AddUIElement,X}". in the textfile. With X being a valid int.
    public int _UIElementNr;
}

public class AddHatEvent : EventInfo
{
    //written as "{AddHat,X}". in the textfile. With X being a valid int.
    public int _hatNr;
}