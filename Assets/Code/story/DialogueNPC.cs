using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueSO[] conversation;
    private DialogueManager manager;
    private bool DialogueInitiated;
    void Start()
    {
        manager=GameObject.Find("DialogueManager").GetComponent<DialogueManager>(); 
    }

    
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !DialogueInitiated)
        {
            manager.InitiatedDialogue(this);
            DialogueInitiated = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.TurnOffDialogue();
            DialogueInitiated = false;
        }
    }
}
