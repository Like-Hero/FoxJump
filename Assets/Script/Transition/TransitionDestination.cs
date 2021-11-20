using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        Enter,
        Exit,
    }
    public DestinationTag destinationTag;
}
