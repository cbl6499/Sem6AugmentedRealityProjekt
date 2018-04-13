using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHandler : MonoBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private int port;
   /* private static NetworkHandler instance;

    public static NetworkHandler Instance {
        get {
            if (instance == null) {
                instance = new NetworkHandler(Network.player.ipAddress);
            }
            return instance;
        }
        set {
            instance = value;
        }
    }
    */
    public int Port{ get; set; }
    public string HostAddress{ get; set; }

    private NetworkHandler(string hostAddress) {
        this.HostAddress = hostAddress;
        this.Port = 6321;
    }

    //Server Methode
  /**  public void SetupHost() {
        GameLobby lobby = GameLobby.Instance;
        lobby.RegisterPlayer("Testname", Network.player.ipAddress);
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        myClient.Connect(Network.player.ipAddress, this.Port);
    }

    //Client Methode
    public void SetupClient() {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Ready, OnReady);
        myClient.Connect(this.HostAddress, port);
    }

    //Client Methode
    public void SendLobbyRegistration(string username) {
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        msg.Message = username;
        myClient.Send(MsgType.AddPlayer, msg);
    }

    //Client Methode
    public void SendReadyMessage(Player p) {
        myClient.Connect(p.Ip, port);
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        msg.Message = "Ready";
        myClient.Send(MsgType.Ready, msg);
    }

    //Client Methode (Listener)
    private void OnReady(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        //do stuff and start game
        Debug.Log("Yay, everyone is ready");
    }

    private void OnConnected(NetworkMessage netMsg) {
        throw new NotImplementedException();
    }
    /*
    //Server Methode
    public void BroadCastReady() {
        GameLobby lobby = GameLobby.Instance;
        List<Player> players = lobby.Players;
        foreach(Player p in players) {
            SendReadyMessage(p);
        }
    }*/

  /*  //Client Methode
    public void ConnectToLobby(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        GameLobby lobby = GameLobby.Instance;
        lobby.RegisterPlayer(msg.Message, msg.Ip);
    }
    */
    //Client Methode
    private Notification CreateConnectionMessage(string username, string userIp) {
        return new Notification(username, userIp);
    }

    // Use this for initialization
  /*  void Start () {
        SetupHost();
        SetupClient();
	}*/
	
	// Update is called once per frame
	void Update () {
		
	}
}
