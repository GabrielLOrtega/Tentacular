using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringContainer : ScriptableObject
{
    public string title;

    [Multiline]
    public string stringVariable = "";
	
}
