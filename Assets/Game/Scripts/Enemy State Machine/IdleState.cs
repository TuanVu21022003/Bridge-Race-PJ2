using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState
{
    private float _timer;
    private float _randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        _timer = 0;
        _randomTime = Random.Range(1f, 2f);
    }

    public void OnExcute(Enemy enemy)
    {
        if(_timer > _randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
        _timer += Time.deltaTime;
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
