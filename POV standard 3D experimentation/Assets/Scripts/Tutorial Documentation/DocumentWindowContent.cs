using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DocumentationWindow", order = 2)]
public class DocumentWindowContent : ScriptableObject
{
    public bool imageAnimated;
    public Sprite img;
    public Sprite[] animFrames;
    public TextAsset text;
}