using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.SetDoor();
    }

    public void OnExcute(Enemy enemy)
    {
        enemy.GoingToDoor();
    }

    public void OnExit(Enemy enemy)
    {

    }
}
