using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.StopFalling();
    }

    public void OnExcute(Enemy enemy)
    {
        enemy.ChangeState(new PatrolState());
    }

    public void OnExit(Enemy enemy)
    {

    }
}
