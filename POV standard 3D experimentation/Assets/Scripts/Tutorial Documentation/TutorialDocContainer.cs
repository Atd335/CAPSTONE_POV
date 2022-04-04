using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TutorialDocContainer", order = 3)]
public class TutorialDocContainer : ScriptableObject
{
    public DocumentWindowContent[] docs;
}