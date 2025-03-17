using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public Camera[] cameras;
    public bool isReadyToShowMonsters= false;
    public void ActivateCameraByIndex(int index)
    {
        if (index < 0 || index >= cameras.Length)
        {
            Debug.LogError("Индекс камеры выходит за пределы массива!");
            return;
        }

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        cameras[index].gameObject.SetActive(true);
        isReadyToShowMonsters = true;
    }
}
