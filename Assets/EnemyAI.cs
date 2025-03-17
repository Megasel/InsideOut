using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public States states = new States();

    [SerializeField] private List<Transform> idlePosition = new List<Transform>();
    [SerializeField] private float duration;
    [SerializeField] private bool isLeftDoor;
    [SerializeField] private bool fourNightScreamer;
    [SerializeField] private SwitchCam switchCam;
    [SerializeField] private AudioSource aud;
    [SerializeField] private Transform cameraTransform;

    private int _posIndex = 0;
    public enum States
    {
        Idle,
        Patrol
    }
    [SerializeField] TimeManagement timeManagement;
    private void OnEnable()
    {
       
    }
    public void SwitchState(States state)
    {
        switch (state)
        {
            case States.Idle:
                try
                {
                    StartCoroutine(TimerBeforeMove());
                }
                catch
                {

                }
              
                
                break;


        }
    }
    IEnumerator TimerBeforeMove()
    {
        
        int rand = Random.Range(0, 60);
        print(rand);
        yield return new WaitForSeconds(rand);
       
        
        if(_posIndex== 3 && fourNightScreamer)
        {
            transform.position = idlePosition[_posIndex].position;
            transform.rotation = idlePosition[_posIndex].rotation;
            StartCoroutine(OnDoor());
        }
        if (_posIndex == 4 && !fourNightScreamer)
        {
            transform.position = idlePosition[_posIndex].position;
            transform.rotation = idlePosition[_posIndex].rotation;
            StartCoroutine(OnDoor());
        }
        else
        {
            while (!switchCam.isReadyToShowMonsters)
            {
                yield return null;
            }
        }
        print("MOVE");
        transform.position = idlePosition[_posIndex].position;
        transform.rotation = idlePosition[_posIndex].rotation;
        _posIndex++;
        switchCam.isReadyToShowMonsters = false;
    }
    //void Patrol(Transform posA, Transform posB)
    //{
    //    Vector3 direction = (posB.position - posA.position).normalized;

    //    // Поворачиваем объект в сторону движения
    //    transform.rotation = Quaternion.LookRotation(direction);

    //    // Двигаем объект от начальной точки к конечной
    //    transform.DOMove(posB.position, duration)
    //        .SetEase(Ease.Linear) // Устанавливаем линейное движение
    //        .OnComplete(() => Patrol(posB, posA));
    //}
    IEnumerator OnDoor()
    {
       
        if (isLeftDoor)
        {
            timeManagement.warningLeftDoor = true;
            Debug.Log("ON Left DOOR!");
        }
        else
        {
            timeManagement.warningRightDoor = true;
            Debug.Log("ON Right DOOR!");

        }
        
        yield return new WaitForSeconds(15);
        if (isLeftDoor && timeManagement.isLeftDoorClosed)
        {
            gameObject.SetActive(false);
            timeManagement.enemyLeftDestroyed = true;
        }
        else if(isLeftDoor && !timeManagement.isLeftDoorClosed)
        {
            Screamer();
            StartCoroutine(timeManagement.GameOver(gameObject.name));
            if (!fourNightScreamer)
                transform.LookAt(cameraTransform.position);
        }
        if (!isLeftDoor && timeManagement.isRightDoorClosed)
        {
            gameObject.SetActive(false);
            timeManagement.enemyLeftDestroyed = true;
        }
        else if (!isLeftDoor && !timeManagement.isRightDoorClosed)
        {
            Screamer();
            StartCoroutine(timeManagement.GameOver(gameObject.name));
            if (!fourNightScreamer)
                transform.LookAt(cameraTransform.position);
        }
    }
    void Screamer()
    {
        transform.position = timeManagement.gameOverEnemyPos.position;
        if(!fourNightScreamer)
            transform.rotation = timeManagement.gameOverEnemyPos.rotation;
        else
            transform.eulerAngles = new Vector3(270,0, 91); 
       

        aud.Play();
    }
}
