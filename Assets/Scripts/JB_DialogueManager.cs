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

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();   
    }

    public void StartDialogue(JB_Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        switch (dialogue.name)
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

        //dialogueSystem.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);

        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log(sentences.Count);

        if(sentences.Count < 1)
        {
            EndDialogue();

            return;

        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(CoTypeSentence(sentence));

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
        //dialogueSystem.SetActive(false);
        Debug.Log("DID WE REACH END");
    }
}
