using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WhoAmIServer: NetworkBehaviour {

    NetworkServer server;
    private int port;
    private string hostAddress;
    private static WhoAmIServer instance;

    public static WhoAmIServer Instance {
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
    public void SetupHost() {
        NetworkServer.Reset();
        this.Port = 63210;
        this.HostAddress = Network.player.ipAddress;
        NetworkServer.Listen(this.Port);
        GameLobby lobby = GameLobby.Instance;
        lobby.SetOwner("127.0.0.1");
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);

        //NetworkServer.RegisterHandler(MsgType.UpdateVars, CheckGuess);

    }

    public void ConnectToLobby(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby lobby = GameLobby.Instance;
        Debug.Log("Test");
        lobby.RegisterPlayer(msg.value);
        if(lobby.Size == lobby.Players.Count) {
            BroadCastReady();
        } else {
            BroadCastConnectedPlayer();
        }
    }

    public void BroadCastReady() {
        GameLobby lobby = GameLobby.Instance;
        List<Player> players = lobby.Players;
        BroadCastMessage(MsgType.LobbyReadyToBegin, "Ready");
    }

    public void BroadCastConnectedPlayer() {
        string players = "";
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;

        BroadCastMessage(MsgType.SyncList, players);
    }

    private void BroadCastMessage(short type, string message) {
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;
        StringMessage msg = new StringMessage();
        msg.value = message;
        NetworkServer.SendToAll(type, msg);
    }

    private void SendMessageToClient(NetworkMessage netMsg, short type, string text) {
        string msg = netMsg.ReadMessage<StringMessage>().value;
        Debug.Log(msg);
        StringMessage answer = new StringMessage();
        answer.value = text;
        NetworkServer.SendToClient(netMsg.conn.connectionId, type, answer);
    }

        //throw new NotImplementedException();
    private void OnConnected(NetworkMessage netMsg) {
        SendMessageToClient(netMsg, MsgType.Connect, "Success");
    }

    public void StartGame() {
        BroadCastMessage(MsgType.LobbySceneLoaded, "Start");
    }
    private void CheckGuess(NetworkMessage netMsg)
    {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby gl = GameLobby.Instance;
        Boolean guessResult = gl.CheckGuess(msg.ToString(), netMsg.conn.connectionId + "");
        StringMessage answer = new StringMessage();
        answer.value = guessResult.ToString();
        NetworkServer.SendToClient(netMsg.conn.connectionId, MsgType.UpdateVars ,answer);
    }
    
    // Use this for initialization
    /*void Start () {
        this.Port = 6321;
        SetupHost();
    }*/
	
	// Update is called once per frame
	void Update () {
		
	}
}
