using DG.Tweening;
using UnityEngine;

public class CamButton : MonoBehaviour
{
    public GameObject camPanel;
    public float startRotation = 330f;
    public float endRotation = 154.02f;
    public float duration = 1f;


    [SerializeField] private Camera securityCam;
    [SerializeField] private Camera locationCam;

    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject leftRightButtons;

    [SerializeField] private TimeManagement timeManagement;

    [SerializeField] private AudioClip securityAmbientSound;
    [SerializeField] private AudioClip camerasAmbientSound;
    [SerializeField] private AudioClip clickSound;

    [SerializeField] private AudioSource audSource;
    [SerializeField] private AudioSource audSourceEvents;

    // Tablet
    

    private bool _isRotated = false;

    public void SwitchCamButton()
    {
        if (_isRotated)
        {
            Switch();
            RotateTo(startRotation);
        }
        else
        {
            RotateTo(endRotation);
        }

        _isRotated = !_isRotated;
    }

    void RotateTo(float targetRotation)
    {
        print("tablet");
        float adjustedRotation = targetRotation;

        Quaternion targetQuaternion = Quaternion.Euler(adjustedRotation, 0, 0);
        
            
        if (!_isRotated)
        {
            audSourceEvents.clip = clickSound;
            audSourceEvents.Play();
            tablet.transform.DOLocalRotateQuaternion(targetQuaternion, duration)
                            .SetEase(Ease.InCubic).OnComplete(Switch); ;
                                                   
            timeManagement.isCamActivated = true;
            
        }
        else
        {
            tablet.transform.DOLocalRotateQuaternion(targetQuaternion, duration)
                .SetEase(Ease.InCubic); 
            timeManagement.isCamActivated = false;
            leftRightButtons.SetActive(true);
            audSource.clip = securityAmbientSound;
            audSource.Play();
        }
        timeManagement.UpdateSlider();
    }

    private void Switch()
    {

        if (camPanel.activeSelf)
        {
            camPanel.SetActive(false);
            locationCam.gameObject.SetActive(false);
            securityCam.gameObject.SetActive(true);
            
        }
        else
        {
            camPanel.SetActive(true);
            locationCam.gameObject.SetActive(true);
            securityCam.gameObject.SetActive(false);
            leftRightButtons.SetActive(false);
            audSource.clip = camerasAmbientSound;
            audSource.Play();
        }
        
    }
}
