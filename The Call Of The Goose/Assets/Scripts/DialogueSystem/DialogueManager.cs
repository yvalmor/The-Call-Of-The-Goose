using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public Animator animator;
        
        public Text nameText;
        public Text dialogueText;
        
        private Queue<string> sentences;
        private static readonly int isOpen = Animator.StringToHash("IsOpen");

        // Start is called before the first frame update
        void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Debug.Log($"Conversation started with {dialogue.name}");
            
            animator.SetBool(isOpen, true);
            
            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
                sentences.Enqueue(sentence);

            nameText.text = dialogue.name;
            
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
        }

        void EndDialogue()
        {
            Debug.Log("Conversation Ended");
            
            animator.SetBool(isOpen, false);
        }
    }
}
