using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public DialogueActors[] actors;

    [Tooltip("Only needed if Random is selected as the actor name")]
    [Header("Random Actor Info")]
    public string randomActorName;
    public Sprite randomActonAvatar;

    [Header("Dialogue")]
    [TextArea]
    public string[] Dialogue;

    [Tooltip("The words that will appear on option buttons")]
    public string[] OptionText;

    public DialogueSO Option0;
    public DialogueSO Option1;
}
