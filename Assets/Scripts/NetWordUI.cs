using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
public class NetWordUI : NetworkBehaviour
{
    [SerializeField]private Button host;
    [SerializeField]private Button clien;
    [SerializeField]private TextMeshProUGUI players;
    private NetworkVariable<int> playerNum=new NetworkVariable<int>();
    // Start is called before the first frame update
    void Start()
    {
        this.host=GetComponent<Button>();
        this.clien=GetComponent<Button>();
    }
    void Awake()
    {
        host.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartHost();
            checkButton();
        });

        clien.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartClient();
            checkButton();
        });
    }

    // Update is called once per frame
    void Update()
    {
        this.networkConnected();

    }
    protected void checkButton()
    {
        Debug.Log(gameObject.name + " was clicked");
    }
    protected void networkConnected()
    {
        this.players.text ="Players : "+ this.playerNum.Value.ToString();
        
        if(!IsServer)return;
        this.playerNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
        
    }
}
