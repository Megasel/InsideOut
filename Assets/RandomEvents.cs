using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    [SerializeField] private TimeManagement timeManagement;
    [SerializeField] private List<MoveTypes> moveTypes;
    [SerializeField] private List<EnemyAI> enemyes = new List<EnemyAI>();
    [SerializeField] private GameObject enemyObject;
    public void EnableEvent()
    {
        int rand = Random.Range(0, moveTypes.Count);
        moveTypes[rand].enabled = true;
        moveTypes.RemoveAt(rand);
    }
 
   
    public void EnemySwitchState()
    {
        foreach(EnemyAI enemy in enemyes)
        {
            enemy.SwitchState(EnemyAI.States.Idle);
        }
    }
}
