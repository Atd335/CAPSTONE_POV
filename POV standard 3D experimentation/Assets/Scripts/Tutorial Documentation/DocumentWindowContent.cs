using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DocumentationWindow", order = 2)]
public class DocumentWindowContent : ScriptableObject
{
    public bool isVideo;
    public Sprite img;
    public TextAsset text;
    public VideoClip videoClip;
}