using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectsEvent : EventInfo
{
    //written as "{ResetObjects,X}". in the textfile. With X being a valid int.
    public int _currentLoop;
}
