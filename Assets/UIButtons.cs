using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject headfonesPanel;
    
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
}
