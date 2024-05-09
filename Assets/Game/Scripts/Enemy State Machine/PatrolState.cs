using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private float _timer;
    private float _randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.SetIsGoToDes(false);
    }

    public void OnExcute(Enemy enemy)
    {
        if(enemy.CheckPickEnough())
        {
            if(enemy.isGotoDes == false)
            {
                enemy.Moving();
            }

        }
        else
        {
            if(enemy.GetRamdomState())
            {
                enemy.ChangeState(new AttackState());

            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
            enemy.SetCountBrickPick(0);
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
