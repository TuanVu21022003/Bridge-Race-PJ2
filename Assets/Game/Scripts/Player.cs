using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    [SerializeField] private VariableJoystick joystick;
    
    // Update is called once per frame
    void Update()
    {
        MoveController();
    }

    public override void OnInit()
    {
        base.OnInit(); 
        speed = KeyConstants.Speed_Player;
    }

    public void MoveController()
    {
        if(PlayManager.instance.endGame)
        {
            speed = 0;
        }
        if(isWin)
        {
            return;
        }
        if(isFalling)
        {
            ChangeAnim(KeyConstants.Anim_Falling);
            return;
        }
        
        MoveJoystick();
        
        
        
    }

    public void MoveJoystick()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        ChangeVisualTotal(movement);
        MoveWhenStair(movement);
        MoveWhenDoor(movement);
        transform.position += movement * speed * Time.deltaTime;
    }

    

    

    public override void Win()
    {
        base.Win();
        
    }

    
}
