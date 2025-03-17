using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAndLight : MonoBehaviour
{
    enum ButtonTypes
    {
        Door,
        Light
    }
    [SerializeField] private GameObject lightPlate;
    [SerializeField] private TimeManagement timeManagement;
    [SerializeField] private Door door;
    [SerializeField]private ButtonTypes buttonTypes;

    [SerializeField] AudioSource aud;
    private void OnMouseDown()
    {
        switch (buttonTypes)
        {
            case ButtonTypes.Door:
                Door();
                break;
                case ButtonTypes.Light:
                Light();
                break;
        }
    }
    public void Door()
    {
       
        
        if (door.isClosed)
        {
            timeManagement.doorsActive--;
        }
        else
        {
            timeManagement.doorsActive++;
        }
        door.Close();
        timeManagement.UpdateSlider();

    }
    public void Light()
    {
        if (lightPlate.activeSelf)
        {
            lightPlate.SetActive(false);
            timeManagement.lightsActive++;
            aud.pitch = 1;
            aud.Play();
        }
        else
        {
            aud.pitch = 0.9f;
            aud.Play();
            lightPlate.SetActive(true);
            timeManagement.lightsActive--;
        }
        timeManagement.UpdateSlider();
    }
}
