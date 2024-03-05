using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform player;
    private Vector3 offset=new Vector3((float)0.06,(float)2.26,(float)-3.1702);

    // Start is called before the first frame update
    void Start()
    {
        this.player=GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.UpdateCamera();
    }
    void UpdateCamera()
    {
        transform.position=this.player.position+offset;
    }
}
