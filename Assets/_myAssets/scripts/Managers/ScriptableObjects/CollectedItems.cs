using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Score", menuName = "Collectible Tracker")]
public class CollectedItems : ScriptableObject
{
    public int score;
    public int resetScore;
    public int endScore;
    //This is how you create a list. Notice how the type
    //is specified in the angle brackets (< >).
    //List<BadGuy> badguys = new List<BadGuy>();
    public List<Collectible> trash = new List<Collectible>();
    
}
