using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBox : MonoBehaviour
{
    [SerializeField] private int _textFileToLoad;
    private DialogueSystemV2 _dialogueSystemV2;
    //an int to keep track of the next line? so I don't have to have different textfiles for each conversation?
    private int _lineToLoad = 0;
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

            _dialogueSystemV2.StartDialogue(_textFileToLoad, _lineToLoad);
            _textFileToLoad += 1;
        }
    }
}
