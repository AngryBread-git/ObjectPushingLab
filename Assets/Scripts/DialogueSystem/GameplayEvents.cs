using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageEvent : EventInfo
{
    //written as "{NextStage}". in the textfile. 
    //the stage nr is tracked by the dialogue trigger.
    public int _nextStageNr;
}

public class AddUIElementEvent : EventInfo
{
    //written as "{AddUIElement,X}". in the textfile. With X being a valid int.
    public int _UIElementNr;
}

public class AddHatEvent : EventInfo
{
    //written as "{AddHat}". in the textfile.
    public int _hatNr;
}

public class TeleportToTopEvent : EventInfo
{
}
public class ResetPositionEvent : EventInfo
{
}

public class PlayDialogueSoundEvent : EventInfo
{
}