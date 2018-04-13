using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WhoAmIServer : MonoBehaviour {

    //private NetworkClient client;
    NetworkServer server;
    private int port;
    private string hostAddress;
    private WhoAmIServer instance;

    public WhoAmIServer Instance {
        get {
            if (instance == null) {
                instance = new WhoAmIServer();
            }
            return instance;
        }
        set {
            instance = value;
        }
    }

    public NetworkServer Server{ get; set; }
    public int Port { get; set; }
    public string HostAddress{ get; set; }

    private WhoAmIServer() {
        this.Port = 6321;
    }

    public void SetupHost() {
        NetworkServer.Listen(this.Port);
        GameLobby lobby = GameLobby.Instance;
        lobby.SetOwner("Diego1337", "127.0.0.1");
        //Server = new NetworkServer();
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        //this.Server.Connect(Network.player.ipAddress, this.Port);
        this.Port = 6321;
        this.HostAddress = Network.player.ipAddress;
    }

    public void ConnectToLobby(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        GameLobby lobby = GameLobby.Instance;
        lobby.RegisterPlayer(msg.Message, msg.Ip);
        BroadCastConnectedPlayer();
    }

    public void BroadCastReady() {
        GameLobby lobby = GameLobby.Instance;
        List<Player> players = lobby.Players;
        BroadCastMessage(MsgType.Ready, "Ready");
    }

    public void BroadCastConnectedPlayer() {
        string players = "";
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;
        foreach(Player p in playerList) {
            players += p.Username + ",";
        }
        BroadCastMessage(MsgType.NotReady, players);
    }

    public void BroadCastMessage(short type, string message) {
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;
        Notification msg = new Notification();
        msg.Message = message;
        /*foreach (Player p in playerList) {
            msg.Ip = p.Ip;
            this.Server.Send(type, msg);
        }*/
        NetworkServer.SendToAll(type, msg);
    }
    private void OnConnected(NetworkMessage netMsg) {
        throw new NotImplementedException();
    }

    public void SendReadyMessage(Player p) {
        //this.Client.Connect(p.Ip, port);
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        msg.Message = "Ready";
        //this.Client.Send(MsgType.Ready, msg);

        
    }

    // Use this for initialization
    void Start () {
        SetupHost();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
