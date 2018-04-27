
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WhoAmIClient : NetworkBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private string username;
    private int port = 63210;
    private static WhoAmIClient instance;
    private int localClient;
    private List<Player> playerList = new List<Player>();

    private short owner = 333;
    private short updateVars = 332;
    private short spawnFinished = 331;
    private short syncList = 330;
    private short connect = 329;
    private short addPlayer = 328;
    private short ready = 327;
    private short lobbyReadyToBegin = 326;

    public int Port { get; set; }
    public string HostAddress { get; set; }
    public string Username { get; set; }

    public NetworkClient MyClient { get; set; }
    public static WhoAmIClient Instance {
        get {
            if (instance == null) {
                instance = new WhoAmIClient();
            }
            return instance;
        }
        set {
            instance = value;
        }
    }

    public void SetupClient() {
        this.MyClient = new NetworkClient();
        this.MyClient.RegisterHandler(ready, OnReady);
        this.MyClient.RegisterHandler(syncList, SetPlayerList);
        this.MyClient.RegisterHandler(connect, OnSuccessfulConnection);
        this.MyClient.RegisterHandler(spawnFinished, SetLocalClient);
        this.MyClient.RegisterHandler(updateVars, GameWon);
        this.MyClient.RegisterHandler(lobbyReadyToBegin, LobbyReady);
        // this.MyClient.RegisterHandler(MsgType.Owner, SetOwner);

        this.Connect();
    }

    private void LobbyReady(NetworkMessage netMsg) {
        Debug.Log("Lobby is Ready to begin!");
    }

    private void OnSuccessfulConnection(NetworkMessage netMsg) {
        this.SendLobbyRegistration();
    }

    public void Connect() {
        Debug.Log("I am connecting");
        this.MyClient.Connect(this.HostAddress, port);
    }

    public void SendLobbyRegistration() {
        this.MyClient.Send(addPlayer, new StringMessage(this.Username));
    }

    public void SendReadyMessage() {
        myClient.Send(ready, new StringMessage("Ready"));
    }

    private void OnReady(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Yay, everyone is ready");
    }

    private void SetLocalClient(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        this.localClient = Int32.Parse(msg.value);
    }

    private void SetPlayerList(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Yay, " + msg.value + " are in the lobby");
        string[] array = msg.value.Split(',');
        //Debug.Log(array[1] + ", " + array[0]);
        foreach (string a in array) {
            string[] player = a.Split('|');
            player[0].Trim();
            if (a != "") {
                Debug.Log(player[0]);
                playerList.Add(new Player(Int32.Parse(player[0]), player[1]));
            }
        }
    }

    private StringMessage CreateConnectionMessage(string username, string userIp) {
        return new StringMessage(username);
    }

    private void SendCorrectGuess() {
        myClient.Send(owner, new StringMessage(localClient.ToString()));
    }

    private void GameWon(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Guess was: " + msg.value);
    }

    public bool CheckGuess(string face) {
        if (playerList[localClient].Face.ToLower() == face.ToLower()) {
            SendCorrectGuess();
            Debug.Log("Guess was Correct");
            return true;
        }
        Debug.Log("The Guess was not correct!");
        return false;
    }
}
