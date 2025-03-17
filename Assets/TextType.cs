using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextType : MonoBehaviour
{
    [SerializeField] public string message;
    public float delay;

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private AudioSource aud;

    
    private void OnEnable()
    {
        StartCoroutine(Type(message));
    }
    public IEnumerator Type(string message)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < message.ToCharArray().Length; i++)
        {
            yield return new WaitForSeconds(0.07f);
            aud.Play();
            messageText.text += message[i];
        }
        
    }
}
