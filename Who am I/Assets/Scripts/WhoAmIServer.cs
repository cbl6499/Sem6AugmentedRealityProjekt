using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
/*
    public WhoAmIServer() {
        this.Port = 6321;
        SetupHost();
    }
    */
    public void SetupHost() {
        NetworkServer.Reset();
        this.Port = 63210;
        this.HostAddress = Network.player.ipAddress;
        NetworkServer.Listen(this.Port);
        GameLobby lobby = GameLobby.Instance;
        lobby.SetOwner("Diego1337", "127.0.0.1");
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);

        NetworkServer.RegisterHandler(MsgType.UpdateVars, CheckGuess);

    }

    public void ConnectToLobby(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        GameLobby lobby = GameLobby.Instance;
        Debug.Log("Test");
        lobby.RegisterPlayer(msg.Message, msg.Ip);
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
        foreach(Player p in playerList) {
            players += p.Username + ",";
        }
        BroadCastMessage(MsgType.SyncList, players);
    }

    private void BroadCastMessage(short type, string message) {
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;
        Notification msg = new Notification();
        msg.Message = message;
        NetworkServer.SendToAll(type, msg);
    }

    private void SendMessageToClient(NetworkMessage netMsg, short type, string text) {
        Notification msg = netMsg.ReadMessage<Notification>();
        Notification answer = new Notification();
        answer.Message = text;
        answer.Ip = msg.Ip;
        NetworkServer.SendToClient(netMsg.conn.connectionId, type, answer);
    }

    private void OnConnected(NetworkMessage netMsg) {
        SendMessageToClient(netMsg, MsgType.Connect, "Success");
        //throw new NotImplementedException();
    }

    public void StartGame() {
        BroadCastMessage(MsgType.LobbySceneLoaded, "Start");
    }

<<<<<<< HEAD
    private void CheckGuess(NetworkMessage netMsg)
    {
        Notification msg = netMsg.ReadMessage<Notification>();
        GameLobby gl = GameLobby.Instance;
        Boolean guessResult = gl.CheckGuess(msg.ToString(), msg.Ip);
        Notification answer = new Notification();
        answer.Message = guessResult.ToString();
        NetworkServer.SendToClient(netMsg.conn.connectionId, MsgType.UpdateVars ,answer);
    }

    // Use this for initialization
    void Start () {
        this.Port = 6321;
        SetupHost();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


=======
>>>>>>> 220dd66f7dec4fba4ac4a70dcfb92e6fcf04a331
}
