using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    [SerializeField] public bool isClosed = false;
    public Vector3 minVector;
    public Vector3 maxVector; 
    [SerializeField] private float duration;
    [SerializeField] AudioSource aud;
    [SerializeField] TimeManagement timeManagement;
    [SerializeField] private bool isLeft;
    public void Close()
    {
        if (isClosed)
        {
            Opened();
            transform.DOMove(minVector, duration).SetEase(Ease.InOutSine);
           
        }
        else
        {
            transform.DOMove(maxVector, duration).SetEase(Ease.InOutSine).OnComplete(() => Closed());
            
        }
    }
    void Closed()
    {
        if (isLeft) 
        { 
            timeManagement.isLeftDoorClosed = true;
        
        }
        else
        {
            timeManagement.isRightDoorClosed = true;

        }
        isClosed = true;
    }
    void Opened()
    {
        if (isLeft)
        {
            timeManagement.isLeftDoorClosed = false;

        }
        else
        {
            timeManagement.isRightDoorClosed = false;

        }
        isClosed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorTrigger"))
        {
            aud.Play();

        }
    }
}
