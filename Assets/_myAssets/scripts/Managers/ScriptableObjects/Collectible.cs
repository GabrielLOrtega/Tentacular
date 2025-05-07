using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Pickup Item", menuName = "Create Item")]
public class Collectible : ScriptableObject
{
    public string type;
    public string description;
    public int value;
    public AudioClip pickupSound;
    public GameObject pickupEffect;
	
}
