using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private GameObject brickVisualPrefab;
    [SerializeField] private Transform parentbrickVisual;
    [SerializeField] protected float speed;
    private ColorBrickEnum _colorPlayer;
    public ColorBrickEnum colorPlayer => _colorPlayer;
    private float sizeBrickVisual;
    private float heightBrick = 0;
    private int _countBrick;
    private List<GameObject> listBrickVisual = new List<GameObject>();
    public int countBrick => _countBrick;
    private string currentAnim = KeyConstants.Anim_Idle;
    private float speedZ;
    protected bool isPreventStair = false;
    protected bool isPreventDoor = false;
   


    private int _floorIndexCurrent = 1;
    public int floorIndexCurrent => _floorIndexCurrent;

    protected bool isWin = false;
    protected bool isFalling = false;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        
    }

    public virtual void OnInit()
    {
        sizeBrickVisual = brickVisualPrefab.GetComponent<MeshRenderer>().bounds.size.y;
        speed = KeyConstants.Speed_Player;
    }

    public void SetColorCharacter(ColorBrickEnum colorPlayerSet)
    {
        _colorPlayer = colorPlayerSet;
        brickVisualPrefab.GetComponent<MeshRenderer>().material = PlayManager.instance.GetMaterial(colorPlayerSet);
        playerVisual.GetComponent<SkinnedMeshRenderer>().material = PlayManager.instance.GetMaterial(colorPlayerSet);
    }

    
    public void SetFloorIndexCurrent()
    {
        _floorIndexCurrent += 1;
    }

    public void GoToDoor(Vector3 doorPos)
    {
        transform.position = doorPos + new Vector3(0, 0, 0.5f);
    }


    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ChangePositionPlayer(Vector3 pos)
    {
        transform.position = new Vector3(transform.position.x, 0.5f + pos.y, transform.position.z);
    }

    public virtual void AddBrick()
    {
        _countBrick++;
        GameObject brick = Instantiate(brickVisualPrefab, parentbrickVisual);
        brick.transform.localPosition = new Vector3(0, heightBrick, 0);
        listBrickVisual.Add(brick);
        heightBrick += sizeBrickVisual;
    }

    public void RemoveBrick()
    {
        _countBrick--;
        GameObject brick = listBrickVisual[listBrickVisual.Count - 1];
        listBrickVisual.RemoveAt(listBrickVisual.Count - 1);
        Destroy(brick);
        heightBrick -= sizeBrickVisual;
    }

    public void ChangeVelocityStair(bool check)
    {
        isPreventStair = check;
    }

    public void ChangeVelocityDoor(bool check)
    {
        isPreventDoor = check;
    }



    public virtual void Win()
    {
        ChangeAnim(KeyConstants.Anim_Win);
        isWin = true;
        rb.velocity = Vector3.zero;
        PlayManager.instance.SetEndGame(true);
        if(this is Player)
        {
            UIManager.Instance.OpenUI<UIVictory>();

        }
        else
        {
            UIManager.Instance.OpenUI<UIFail>();

        }

    }

    public virtual void MoveWhenStair(Vector3 movement)
    {

        float direction = movement.z;
        if (direction > 0)
        {
            if (isPreventStair)
            {
                speed = 0;

            }
        }
        else if (direction < 0)
        {
            speed = KeyConstants.Speed_Player;
        }

    }

    public virtual void MoveWhenDoor(Vector3 movement)
    {
        float direction = movement.z;
        if (direction < 0)
        {
            if (isPreventDoor)
            {
                speed = 0;

            }
        }
        else if (direction > 0 && !isPreventStair)
        {
            speed = KeyConstants.Speed_Player;
        }
    }

    public void ChangeVisualTotal(Vector3 movement)
    {
        if (movement.x != 0 || movement.z != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            ChangeAnim(KeyConstants.Anim_Run);
            parentbrickVisual.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            ChangeAnim(KeyConstants.Anim_Idle);
            parentbrickVisual.localRotation = Quaternion.Euler(-13f, 0, 0);

        }
    }

    public virtual void ActionNext()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag(KeyConstants.Tag_Player) && collision.gameObject.GetComponent<Character>().countBrick > this.countBrick)
    //    {
    //        isFalling = true;
    //        rb.velocity = Vector3.zero;
    //        MapController.Instance.GetFloorByLevel(floorIndexCurrent).GetComponent<FloorController>().InstantiateBrickGray(_countBrick, this.transform.position);
    //        ClearBrick();

    //        Invoke(nameof(ResetFalling), 4f);
    //    }
    //}

    public void ResetFalling()
    {
        isFalling = false;
        
    }

    public void ClearBrick()
    {
        while(_countBrick > 0)
        {
            RemoveBrick();
        }
    }
}
