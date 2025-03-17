using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;

    private const float _tolerance = 0.1f;

    public void RotateLeft()
    {
        if (IsAngleApproximately(transform.eulerAngles.y, 180) || IsAngleApproximately(transform.eulerAngles.y, 245.5f))
        {
           
            transform.DORotate(new Vector3(0, -65.5f, 0), rotateSpeed, RotateMode.LocalAxisAdd);
        }
    }
    public void RotateCenter()
    {
        transform.DOLocalRotate(new Vector3(0, 180, 0), rotateSpeed);
    }
    public void RotateRight()
    {
       
        if (IsAngleApproximately(transform.eulerAngles.y, 180) || IsAngleApproximately(transform.eulerAngles.y, 114.5f))
        {
            
            transform.DORotate(new Vector3(0, 65.5f, 0), rotateSpeed, RotateMode.LocalAxisAdd);
        }
    }

    private bool IsAngleApproximately(float angle, float target)
    {
        
        return Mathf.Abs(angle - target) < _tolerance;
    }
    
    
}
