using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_DialogueTrigger : MonoBehaviour
{
    public JB_Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<JB_DialogueManager>().StartDialogue(dialogue);
    }
}
