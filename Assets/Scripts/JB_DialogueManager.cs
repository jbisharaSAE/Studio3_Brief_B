using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JB_DialogueManager : MonoBehaviour
{
    public Animator animator;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public Sprite bob;
    public Sprite tot;

    public Image characterImage;
    public GameObject dialogueSystem;

    private Queue<string> sentences;
    private Queue<string> names;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();   
    }

    public void StartDialogue(JB_Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        // clearing current queues to make sure we dont repeat previous dialogues
        names.Clear();
        sentences.Clear();

        // initialising values from dialogue class into queue variable
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            
        }
        
        // initialising values from dialogue class into queue variable
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        // begin telling story, using continue button to go thru next dialogue
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // end dialogue
        if(sentences.Count < 1)
        {
            EndDialogue();

            return;

        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();

        nameText.text = name;

        DisplayCharacterSprite(name);

        StopAllCoroutines();
        StartCoroutine(CoTypeSentence(sentence));

    }

    private void DisplayCharacterSprite(string name)
    {
        switch (name)
        {
            case "Bob":
                characterImage.sprite = bob;
                break;
            case "Tot":
                characterImage.sprite = tot;
                break;
            default:
                break;
        }

    }

    IEnumerator CoTypeSentence(string sentence)
    {
        yield return new WaitForSeconds(0.2f);
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        
    }
}
