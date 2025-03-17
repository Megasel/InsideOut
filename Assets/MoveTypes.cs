using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTypes : MonoBehaviour
{
    public enum MovingType
    {
        Rotation,
        Rotation360,
        Levitation,
        RandomRotation,
        Moving,
        Bar,
        Piano


    }
    [SerializeField] private MovingType movingType;
    [SerializeField] private bool onStart;
    [SerializeField] Transform barTransform;
    [SerializeField] AudioSource aud;
    [SerializeField] private bool isLocalRotate;

    public Vector3 minVector;
    public Vector3 maxVector;
    public float duration = 2f;

    
   
    private void Start()
    {
        if (onStart)
        {
            switch (movingType)
            {
                case MovingType.Rotation:
                    Rotation();
                    break;
                case MovingType.Rotation360:
                    Rotation360();
                    break;
                case MovingType.Moving:
                    Moving();
                    break;
                case MovingType.Bar:
                    Bar();
                    break;
                case MovingType.Levitation:
                    Levitation();
                    break;
                case MovingType.Piano:
                    Piano();
                    break;

            }
        }
        
    }
    void Levitation()
    {
        transform.DOLocalMove(maxVector, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    void Bar()
    {
        transform.position = barTransform.position;
        transform.rotation = barTransform.rotation;
    }
    void Rotation()
    {
        if (isLocalRotate)
        {
            transform.localRotation = Quaternion.Euler(minVector);


            transform.DOLocalRotate(maxVector, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            transform.rotation = Quaternion.Euler(minVector);

           
            transform.DORotate(maxVector, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); 
        }
    }
    void Rotation360()
    {
        transform.DOLocalRotate(new Vector3(0, 0, -360), duration, RotateMode.FastBeyond360)
                  .SetEase(Ease.Linear)  
                  .SetLoops(-1, LoopType.Restart); 
    }
    void Moving()
    {
        transform.DOMove(maxVector, duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    void Piano()
    {
        if (isLocalRotate)
        {
            transform.localRotation = Quaternion.Euler(minVector);


            transform.DOLocalRotate(maxVector, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); 
                
        }
        else
        {
            transform.rotation = Quaternion.Euler(minVector);

           
            transform.DORotate(maxVector, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
              
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Piano") && aud.gameObject.activeSelf)
        {
            aud.Play();
        }
    }
}
