using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : Character
{

    [SerializeField] NavMeshAgent _navMeshAgent;

    private IState currentState;
    private Transform _destination;
    private bool _isGotoDes = false;
    private Transform destionationDoor;
    [SerializeField] private int enemyIndex;
    public bool isGotoDes => _isGotoDes;
    private int _countBrickPer = 3;
    public int countBrickPer => _countBrickPer;
    private int _countBrickPick = 0;
    public int countBrickPick => _countBrickPick;

    [ContextMenu("GoToBrick")]
    public void GoToBrick()
    {
        GameObject brickBot = FindBrick();
        if(brickBot != null )
        {
            _destination = brickBot.transform;
            SetIsGoToDes(true);
            if (_navMeshAgent == null)
            {
                Debug.Log("ko");
            }
            else
            {
                SetDestiantion(Vector3.zero);
            }
        }
        else
        {
            ChangeState(new AttackState());
        }
    }

    public void SetIsGoToDes(bool isGoToDes)
    {
        this._isGotoDes = isGoToDes;
    }
    public override void OnInit()
    {
        base.OnInit();
        SetDestionationDoor();
        ChangeState(new PatrolState());

    }

    private void SetDestiantion(Vector3 offset)
    {
        if(_destination != null)
        {
            Vector3 targetVector = _destination.position + offset;
            _navMeshAgent.SetDestination(targetVector);
            StartCoroutine(CheckDestinationReached());
        }

    }

    IEnumerator CheckDestinationReached()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, _destination.position) <= 0.5f)
            {
                // Nhân vật đã đến gần đúng điểm đích
                
                // Thực hiện hành động sau khi đến điểm đích ở đây
                SetIsGoToDes(false);
                // Kết thúc coroutine
                yield break;
            }

            yield return null;
        }
    }

    private void Update()
    {
        if (PlayManager.instance.endGame)
        {
            StopMovingWin();
            return;
        }
        if (isWin)
        {
            return;
        }
        if (isFalling)
        {
            ChangeState(new FallingState());
            return;
        }
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GoToBrick();
        //}
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void StopMoving()
    {
        _destination = transform;
        SetDestiantion(Vector3.zero);
        SetIsGoToDes(false);
        ChangeAnim(KeyConstants.Anim_Idle);
    }

    public void StopFalling()
    {
        _destination = transform;
        SetDestiantion(Vector3.zero);
        SetIsGoToDes(false);
        ChangeAnim(KeyConstants.Anim_Falling);
    }

    public void StopMovingWin()
    {
        _destination = transform;
        SetDestiantion(Vector3.zero);
        SetIsGoToDes(false);
    }

    public void Moving()
    {
        GoToBrick();
        Vector3 movement = transform.forward;
        ChangeVisualTotal(movement);
    }

    public GameObject FindBrick()
    {
        FloorController floorCurrent = MapController.Instance.GetFloorByLevel(floorIndexCurrent).GetComponent<FloorController>();
        return floorCurrent.RandomBrickBot(colorPlayer);
    }

    public override void AddBrick()
    {
        base.AddBrick();
        _countBrickPick++;
    }

    public bool CheckPickEnough()
    {
        return _countBrickPick < Random.Range(_countBrickPer, _countBrickPer + 3);
    }

    public void SetCountBrickPick(int count)
    {
        _countBrickPick = count;
    }

    public bool GetRamdomState()
    {
        int randomNumber = Random.Range(0, 2);
        return randomNumber == 1;
    }

    public void SetDestionationDoor()
    {
        if(floorIndexCurrent < MapController.Instance.countFloor - 1)
        {
            FloorController floor = MapController.Instance.GetFloorByLevel(floorIndexCurrent + 1).GetComponent<FloorController>();
            destionationDoor = floor.GetDoor().transform;
        }
        else
        {
            destionationDoor = MapController.Instance.GetFloorByLevel(floorIndexCurrent + 1).GetComponent<FinalController>().chest.transform;
        }
    }

    public void SetDoor()
    {
        _destination = destionationDoor;
        SetDestiantion(new Vector3(0, 0, 0.5f));
    }

    public void GoingToDoor()
    {
        Vector3 movement = transform.forward;
        ChangeVisualTotal(movement);
        MoveWhenStair(movement);
        MoveWhenDoor(movement);
    }

    public override void MoveWhenStair(Vector3 movement)
    {
        float direction = movement.z;
        if (direction > 0)
        {
            if (isPreventStair)
            {
                StopMoving();
                ChangeState(new PatrolState());

            }
        }
    }

    public override void MoveWhenDoor(Vector3 movement)
    {
        base.MoveWhenDoor(movement);
    }

    public override void ActionNext()
    {
        base.ActionNext();
        ChangeState(new PatrolState());
        SetDestionationDoor();
    }

    public override void Win()
    {
        StopMoving();
        base.Win();
    }

    
}
