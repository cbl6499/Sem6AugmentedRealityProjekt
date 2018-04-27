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
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, ConnectToLobby);
        NetworkServer.RegisterHandler(MsgType.Owner, CreateLobby);
        NetworkServer.RegisterHandler(MsgType.UpdateVars, BroadCastPlayerFinished);
    }

    public void CreateLobby(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby lobby = GameLobby.Instance;
        lobby.SetOwner(msg.value);
        SendMessageToClient(netMsg, MsgType.Owner, "Lobby created");
    }

    public void ConnectToLobby(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby lobby = GameLobby.Instance;
        Player player = lobby.CreatePlayer(msg.value);
        lobby.RegisterPlayer(player);
        SendMessageToClient(netMsg, MsgType.SpawnFinished, player.Number + "");
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
        NetworkServer.SendToAll(type, new StringMessage(message));
    }

    private void SendMessageToClient(NetworkMessage netMsg, short type, string text) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        NetworkServer.SendToClient(netMsg.conn.connectionId, type, new StringMessage(text));
    }

    private void OnConnected(NetworkMessage netMsg) {
        SendMessageToClient(netMsg, MsgType.Connect, "Success");
    }

    public void StartGame() {
        BroadCastMessage(MsgType.LobbySceneLoaded, "Start");
    }

    public void BroadCastPlayerFinished(NetworkMessage netMsg) {
        StringMessage message = netMsg.ReadMessage<StringMessage>();
        int id = Int32.Parse(message.value);

    }

}
