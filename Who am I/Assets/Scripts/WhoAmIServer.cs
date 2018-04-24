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
<<<<<<< HEAD
        lobby.SetOwner(Network.player.ipAddress);
=======
        lobby.SetOwner("127.0.0.1");
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);

        //NetworkServer.RegisterHandler(MsgType.UpdateVars, CheckGuess);

    }

    public void ConnectToLobby(NetworkMessage netMsg) {
<<<<<<< HEAD
        GameLobby lobby = GameLobby.Instance;
        Notification notification = new Notification();
        notification.Deserialize(netMsg.reader);
        Debug.Log(netMsg.ReadMessage<Notification>());
        Debug.Log(notification.Message);
=======
        //StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby lobby = GameLobby.Instance;
        //Debug.Log("Test");
        Debug.Log(netMsg.ReadMessage<StringMessage>().value);
        //lobby.RegisterPlayer(msg.value);
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
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
<<<<<<< HEAD
=======

>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
        BroadCastMessage(MsgType.SyncList, players);
    }

    private void BroadCastMessage(short type, string message) {
<<<<<<< HEAD
        NetworkServer.SendToAll(type, new Notification(message));
    }

    private void SendMessageToClient(NetworkMessage netMsg, short type, string text) {
        Notification msg = netMsg.ReadMessage<Notification>();
        NetworkServer.SendToClient(netMsg.conn.connectionId, type, new Notification(text));
=======
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;
        StringMessage msg = new StringMessage();
        msg.value = message;
        NetworkServer.SendToAll(type, msg);
    }

    private void SendMessageToClient(NetworkMessage netMsg, short type, string text) {
        NetworkServer.SendToClient(netMsg.conn.connectionId, type, new StringMessage(text));
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
    }

        //throw new NotImplementedException();
    private void OnConnected(NetworkMessage netMsg) {
        Debug.Log("Player connected server!");
        SendMessageToClient(netMsg, MsgType.Connect, "Success");
    }

    public void StartGame() {
        BroadCastMessage(MsgType.LobbySceneLoaded, "Start");
    }
<<<<<<< HEAD


=======
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
    private void CheckGuess(NetworkMessage netMsg)
    {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby gl = GameLobby.Instance;
<<<<<<< HEAD
        Boolean guessResult = gl.CheckGuess(msg.ToString(), msg.ToString());
        Notification answer = new Notification();
        answer.Message = guessResult.ToString();
=======
        Boolean guessResult = gl.CheckGuess(msg.ToString(), netMsg.conn.connectionId + "");
        StringMessage answer = new StringMessage();
        answer.value = guessResult.ToString();
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
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
<<<<<<< HEAD



=======
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
}
