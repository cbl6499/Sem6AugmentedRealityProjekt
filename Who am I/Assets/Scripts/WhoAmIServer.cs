using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WhoAmIServer : MonoBehaviour {

    private NetworkClient client;
    private int port;
    private int hostAddress;

    public NetworkClient Client{ get; set; }
    public int Port { get; set; }
    public string HostAddress{ get; set; }

    public void SetupHost() {
        GameLobby lobby = GameLobby.Instance;
        lobby.RegisterPlayer("Testname", Network.player.ipAddress);
        this.Client = new NetworkClient();
        this.Client.RegisterHandler(MsgType.Connect, OnConnected);
        this.Client.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        this.Client.Connect(Network.player.ipAddress, this.Port);
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
        foreach (Player p in players) {
            SendReadyMessage(p);
        }
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
        foreach (Player p in playerList) {
            msg.Ip = p.Ip;
            this.Client.Send(type, msg);
        }
    }
    private void OnConnected(NetworkMessage netMsg) {
        throw new NotImplementedException();
    }

    public void SendReadyMessage(Player p) {
        this.Client.Connect(p.Ip, port);
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        msg.Message = "Ready";
        this.Client.Send(MsgType.Ready, msg);
        
    }

    // Use this for initialization
    void Start () {
        SetupHost();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
