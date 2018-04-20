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
        lobby.SetOwner("Diego1337", "127.0.0.1");
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        
    }

    public void ConnectToLobby(NetworkMessage netMsg) {
        GameLobby lobby = GameLobby.Instance;
        
        Debug.Log(netMsg.ReadMessage<Notification>().Message);
        
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
        NetworkServer.SendToAll(type, new Notification(message));
    }

    private void SendMessageToClient(NetworkMessage netMsg, short type, string text) {
        Notification msg = netMsg.ReadMessage<Notification>();
        NetworkServer.SendToClient(netMsg.conn.connectionId, type, new Notification(text));
    }

    private void OnConnected(NetworkMessage netMsg) {
        Debug.Log("Player connected server!");
        SendMessageToClient(netMsg, MsgType.Connect, "Success");
    }

    public void StartGame() {
        BroadCastMessage(MsgType.LobbySceneLoaded, "Start");
    }

}
