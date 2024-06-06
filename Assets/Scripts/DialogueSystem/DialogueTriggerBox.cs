using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBox : MonoBehaviour
{
    [SerializeField] private int _mainTextFileToLoad;
    [SerializeField] private int _sideTextFileToLoad;
    private DialogueSystemV2 _dialogueSystemV2;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueSystemV2 = FindObjectOfType<DialogueSystemV2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("DialogueTrigger, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder"))
        {

            _dialogueSystemV2.StartDialogue(_mainTextFileToLoad, 0, "main");
            _mainTextFileToLoad += 1;
        }
    }

    public void StartSideDialogue() 
    {
        _dialogueSystemV2.StartDialogue(_sideTextFileToLoad, 0, "side");
        _sideTextFileToLoad += 1;
    }
}
