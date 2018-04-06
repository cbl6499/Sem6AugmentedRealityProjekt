using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHandler : MonoBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private int hostPort;
    private static NetworkHandler instance;

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

    public int HostPort{ get; set; }
    public string HostAddress{ get; set; }

    private NetworkHandler(string hostAddress) {
        this.HostAddress = hostAddress;
        this.HostPort = 6321;
    }

    public void SetupHost() {
        GameLobby lobby = GameLobby.Instance;
        lobby.RegisterPlayer("Testname", Network.player.ipAddress);
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        myClient.Connect(Network.player.ipAddress, this.HostPort);
    }

    public void SetupClient() {
        myClient = new NetworkClient();
        //myClient.RegisterHandler(MsgType.Connect, OnConnected);
        //myClient.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        myClient.RegisterHandler(MsgType.Ready, OnReady);
        myClient.Connect(this.HostAddress, hostPort);
    }

    public void SendLobbyRegistration(string username) {
        ConnectionMessage msg = new ConnectionMessage();
        msg.Ip = Network.player.ipAddress;
        msg.User = username;
        myClient.Send(MsgType.AddPlayer, msg);
    }

    public void SendReadyMessage(Player p) {
        myClient.Connect(p.Ip, hostPort);
        ConnectionMessage msg = new ConnectionMessage();
        msg.Ip = Network.player.ipAddress;
        msg.User = "Ready";
        myClient.Send(MsgType.Ready, msg);
    }

    private void OnReady(NetworkMessage netMsg) {
        ConnectionMessage msg = netMsg.ReadMessage<ConnectionMessage>();
        //do stuff and start game
        Debug.Log("Yay, everyone is ready");
    }

    private void OnConnected(NetworkMessage netMsg) {
        throw new NotImplementedException();
    }

    public void BroadCastReady() {
        GameLobby lobby = GameLobby.Instance;
        List<Player> players = lobby.Players;
        foreach(Player p in players) {
            SendReadyMessage(p);
        }
    }

    public void ConnectToLobby(NetworkMessage netMsg) {
        ConnectionMessage msg = netMsg.ReadMessage<ConnectionMessage>();
        GameLobby lobby = GameLobby.Instance;
        lobby.RegisterPlayer(msg.User, msg.Ip);
    }

    private ConnectionMessage CreateConnectionMessage(string username, string userIp) {
        return new ConnectionMessage(username, userIp);
    }

    // Use this for initialization
    void Start () {
        SetupHost();
        SetupClient();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
