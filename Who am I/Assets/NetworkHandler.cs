using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHandler : MonoBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private int hostPort;

    public int HostPort{ get; set; }
    public string HostAddress{ get; set; }

    public NetworkHandler(string hostAddress, int hostPort) {
        this.HostAddress = hostAddress;
        this.HostPort = hostPort;
    }

    public void SetupClient() {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        myClient.Connect(this.HostAddress, this.HostPort);
    }

    private void OnConnected(NetworkMessage netMsg) {
        throw new NotImplementedException();
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
        SetupClient();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
