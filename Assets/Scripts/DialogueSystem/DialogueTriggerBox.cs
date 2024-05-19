using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBox : MonoBehaviour
{
    [SerializeField] private int _textFileToLoad;
    [SerializeField] private DialogueSystemV2 _dialogueSystemV2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("DialogueTrigger, other.tag is: {0}", other.tag));
        if (other.CompareTag("Boulder"))
        {

            _dialogueSystemV2.StartDialogue(_textFileToLoad, 0);
            _textFileToLoad += 1;
        }
    }
}
