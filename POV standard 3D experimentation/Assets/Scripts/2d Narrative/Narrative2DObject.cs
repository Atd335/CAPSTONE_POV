using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Narrative2D", order = 1)]
public class Narrative2DObject : ScriptableObject
{
    public bool repeat = false;

    public TextAsset textAsset;
    
    public float volume = .15f;
    public float pitch = 1;
}