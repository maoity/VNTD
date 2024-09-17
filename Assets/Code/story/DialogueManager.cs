using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private DialogueSO CurrentConversation;
    private int StepNum;

    private GameObject DialogueCanvas;
    private TMP_Text actor;
    private Image Avatar;
    private TMP_Text DialogueText;
    [SerializeField] private bool DialogueActivated;

    private string currentSpeaker;
    private Sprite currentAvatar;

    public ActorSO[] actorSO;

    private GameObject[] optionButton;
    private TMP_Text[] optionButtonText;
    private GameObject optionPanel;
    void Start()
    {
        optionButton = GameObject.FindGameObjectsWithTag("OptionButton");
        optionPanel = GameObject.Find("OptionsPanel");
        optionPanel.SetActive(false);   

        optionButtonText=new TMP_Text[optionButton.Length];
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButtonText[i]=optionButton[i].GetComponentInChildren<TMP_Text>(); 
        }


        DialogueCanvas = GameObject.Find("Canvas");
        actor=GameObject.Find("Speaker").GetComponent<TMP_Text>();
        Avatar = GameObject.Find("Avatar").GetComponent<Image>();
        DialogueText=GameObject.Find("DialogueText").GetComponent<TMP_Text>();  

        DialogueCanvas.SetActive(false);
        
        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(true);
        }
    }

    void Update()
    {
        if (DialogueActivated&&Input.GetKeyDown(KeyCode.E))
        {
            if (StepNum >= CurrentConversation.actors.Length)
            {
                TurnOffDialogue();
            }
            else
            {
                PlayDialogue();
            }
        }
    }
    void PlayDialogue()
    {
        if (CurrentConversation.actors[StepNum] == DialogueActors.Random)
        {
            SetActorInfo(false);
        }
        else
        {
            SetActorInfo(true);    
        }

        actor.text = currentSpeaker;
        Avatar.sprite = currentAvatar;

        if (CurrentConversation.actors[StepNum] == DialogueActors.Branch)
        {
            for (int i = 0; i < CurrentConversation.OptionText.Length; i++)
            {
                if (CurrentConversation.OptionText[i] == null)
                {
                    optionButton[i].SetActive(false);
                }
                else
                {
                    optionButtonText[i].text=CurrentConversation.OptionText[i];
                    optionButton[i].SetActive(true);    
                }
                optionButton[0].GetComponent<Button>().Select();
               
            }
        }
        if (StepNum < CurrentConversation.Dialogue.Length)
        {
            DialogueText.text = CurrentConversation.Dialogue[StepNum];
        }
        else
        {
            optionPanel.SetActive(true);
        }
        DialogueCanvas.SetActive(true);
        StepNum += 1;
    }

    void SetActorInfo(bool recurringCharacter)
    {
        if (recurringCharacter)
        {
            for (int i = 0; i < actorSO.Length; i++)
            {
                if (actorSO[i].name == CurrentConversation.actors[StepNum].ToString())
                {
                    currentSpeaker=actorSO[i].actorName;
                    currentAvatar = actorSO[i].Avatar;
                }
            }
        }
        else
        {
            currentSpeaker=CurrentConversation.randomActorName;
            currentAvatar=CurrentConversation.randomActonAvatar;
        }
    }

    public void Option(int OptionNum)
    {
        foreach (GameObject button in optionButton)
        {
            button.SetActive(false);
        }
        if (OptionNum == 0)
        {
            CurrentConversation = CurrentConversation.Option0;
            Debug.Log("Continue next Branch:" + StepNum);
        }
        if (OptionNum == 1)
        {
            CurrentConversation = CurrentConversation.Option1;
            Debug.Log("Continue next Branch:"+StepNum);
        }

        StepNum = 0;
        PlayDialogue();
    }
    public void InitiatedDialogue(DialogueNPC NpcDialogue)
    {
        CurrentConversation = NpcDialogue.conversation[0];
        DialogueActivated = true;
    }
    public void TurnOffDialogue()
    {
        StepNum = 0;
        DialogueActivated = false;
        optionPanel.SetActive(false);
        DialogueCanvas.SetActive(false);
    }
}
public enum DialogueActors
{
    Circle,
    Cube,
    Capsule,
    Random,
    Branch,
};