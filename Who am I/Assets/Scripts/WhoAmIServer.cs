using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WhoAmIServer : NetworkBehaviour {

    NetworkServer server;
    private int port;
    private string hostAddress;
    private static WhoAmIServer instance;

    private short owner = 333;
    private short updateVars = 332;
    private short spawnFinished = 331;
    private short syncList = 330;
    private short connect = 329;
    private short addPlayer = 328;
    private short ready = 327;
    private short lobbyReadyToBegin = 326;
    private short playerGuessedRight = 325;
    private short resultMessage = 324;

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

    public NetworkServer Server { get; set; }
    public int Port { get; set; }
    public string HostAddress { get; set; }
    public void SetupHost() {
        NetworkServer.Reset();
        this.Port = 63210;
        this.HostAddress = Network.player.ipAddress;
        NetworkServer.Listen(this.Port);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(addPlayer, ConnectToLobby);
        NetworkServer.RegisterHandler(owner, CreateLobby);
        NetworkServer.RegisterHandler(updateVars, BroadCastPlayerFinished);
       // NetworkServer.RegisterHandler(MsgType.Connect, AcceptConnection);
    }

   /* public void AcceptConnection() {
        OnConnected();
    }*/
    public void CreateLobby(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby lobby = GameLobby.Instance;
        lobby.SetOwner(msg.value);
        SendMessageToClient(netMsg, owner, "Lobby created");
    }

    public void ConnectToLobby(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        GameLobby lobby = GameLobby.Instance;
        Player player = lobby.CreatePlayer(msg.value);
        lobby.RegisterPlayer(player);
        SendMessageToClient(netMsg, spawnFinished, player.Number + "");
        if (lobby.Size == lobby.Players.Count) {
            BroadCastReady();
        } else {
            BroadCastConnectedPlayer();
        }
    }

    public void BroadCastReady() {
        GameLobby lobby = GameLobby.Instance;
        List<Player> players = lobby.Players;
        BroadCastMessage(lobbyReadyToBegin, "Ready");
    }

    public void BroadCastConnectedPlayer() {
        string players = "";
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;
        foreach (Player p in playerList) {
            players += p.Number + "|" + p.Username + ",";
        }
        BroadCastMessage(syncList, players);
    }

    private void BroadCastMessage(short type, string message) {
        GameLobby lobby = GameLobby.Instance;
        List<Player> playerList = lobby.Players;
        NetworkServer.SendToAll(type, new StringMessage(message));
    }

    private void SendMessageToClient(NetworkMessage netMsg, short type, string text) {
        NetworkServer.SendToClient(netMsg.conn.connectionId, type, new StringMessage(text));
    }

    private void OnConnected(NetworkMessage netMsg) {
        NetworkServer.SendToClient(netMsg.conn.connectionId, connect, new StringMessage("Success"));
       // SendMessageToClient(netMsg, connect, "Success");
    }

    public void StartGame() {
        BroadCastMessage(MsgType.LobbySceneLoaded, "Start");
    }

    public void BroadCastPlayerFinished(NetworkMessage netMsg) {
        StringMessage message = netMsg.ReadMessage<StringMessage>();
        int id = Int32.Parse(message.value);
        Player p = findPlayerById(id);
        if (p != null) {
            GameLobby.Instance.PlayersFinished++;
            p.AddPoints(GameLobby.Instance.Players.Count - GameLobby.Instance.PlayersFinished);
            BroadCastMessage(playerGuessedRight, p.Number + "");
        }
    }

    public void BroadCastResult() {
        string result = "";
        foreach (Player p in GameLobby.Instance.Players) {
            result += p.Number + "|" + p.Points;
        }
        BroadCastMessage(resultMessage, result);
    }

    private Player findPlayerById(int id) {
        foreach (Player p in GameLobby.Instance.Players) {
            if (p.Number == id) {
                return p;
            }
        }
        return null;
    }

}
