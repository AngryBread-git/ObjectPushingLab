using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOverEvent : EventInfo
{
    //written as "{StartOver,X}". in the textfile. With X being a valid int.
    public int _loopToStart;
}
