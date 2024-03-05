using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class playerCtrl : NetworkBehaviour
{
    [SerializeField]private float PressVertical=0f;
    [SerializeField]private float PressHorizontal=0f;
    [SerializeField]private float speed = 1f;
    [SerializeField]private float speedHorizon=3f;
    [SerializeField]private float speedUp=0.1f;
    [SerializeField]private float MaxSpeed=5f;
    [SerializeField]private float speedWalk;
    [SerializeField]private float speedRun;
    [SerializeField]private float speedDown=0.02f;
    [SerializeField]private float sum;
    public Vector3 Velocity=new Vector3(0,0,0);
    public Animator walk;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        this.walk=GetComponent<Animator>();
        //this.player=GameObject.Find("Eve By J.Gonzales");
    }

    // Update is called once per frame
    void Update()
    {

        if(!IsOwner)return;

        this.InputManager();
        
        this.walkAnim();
        
    }
    void FixedUpdate()
    {
        this.updateMoving();
    }


    protected void InputManager()
    {
        this.PressHorizontal=Input.GetAxis("Horizontal");
        this.PressVertical=Input.GetAxis("Vertical");
    }


    protected void updateMoving()
    {
        Vector3 pos=new Vector3(0,0,0);
        this.player.transform.Translate(pos+this.Velocity*Time.deltaTime*this.PressVertical);
        this.player.transform.Rotate(Vector3.up*this.speedHorizon*this.PressHorizontal);
        if(this.PressVertical>0)
        {
            Velocity.z+=this.speedUp;
            if(Velocity.z>this.MaxSpeed)
            {
                Velocity.z=MaxSpeed;
            }
        }
        if(this.PressVertical==0)
        {
            Velocity.z-=this.speedUp;
            if(this.Velocity.z<0)
            {
                Velocity.z=0;
            }
            
        }
    }
    protected void walkAnim()
    {
        if(this.PressVertical>0)
        {
            walk.SetFloat("move",0.5f);
        }
        if(this.PressVertical==0&&this.Velocity.z==0)
        {
            walk.SetFloat("move",0);
        }
        if(this.PressVertical==0&&this.Velocity.z<=3&&this.Velocity.z>0)
        {
            walk.SetFloat("move",0.4f);
        }

        if(this.PressVertical>0&&this.Velocity.z>3)
        {
            this.walk.SetFloat("move",0.6f);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            this.walk.SetInteger("punch",1);
        }
        else
        {
            this.walk.SetInteger("punch",0);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.walk.SetInteger("kick",1);
        }
        else
        {
            this.walk.SetInteger("kick",0);
        }
    }
}
