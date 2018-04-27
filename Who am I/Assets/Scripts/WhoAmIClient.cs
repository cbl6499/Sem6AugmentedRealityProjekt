
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
    private List<Player>playerList = new List<Player>();

    public int Port { get; set; }
    public string HostAddress { get; set; }
    public string Username { get; set; }

    void Start() {
     //   SetupClient();
    }

    void Update() {

    }

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
        this.MyClient.RegisterHandler(MsgType.Ready, OnReady);
        this.MyClient.RegisterHandler(MsgType.NotReady, PrintPlayerList);
        this.MyClient.RegisterHandler(MsgType.Connect, OnSuccessfulConnection);
        
        this.MyClient.RegisterHandler(MsgType.UpdateVars, GameWon);
        
        this.Connect();
    }

    private void OnSuccessfulConnection(NetworkMessage netMsg) {
        StringMessage msgBase = netMsg.ReadMessage<StringMessage>();
        Debug.Log("hell yeah " + netMsg.conn.address);
        this.SendLobbyRegistration();
    }

    public void Connect() {
        Debug.Log("I am connecting");
        this.MyClient.Connect(this.HostAddress, port);
    }

    public void SendLobbyRegistration() {
        this.MyClient.Send(MsgType.AddPlayer, new StringMessage(this.Username));
    }

    public void SendReadyMessage() {
        myClient.Send(MsgType.Ready, new StringMessage("Ready"));
    }

    private void OnReady(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Yay, everyone is ready");
    }

    private void PrintPlayerList(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Yay, " + msg.value + " are in the lobby");
    }

    private StringMessage CreateConnectionMessage(string username, string userIp) {
        return new StringMessage(username);
    }

    private void SendCorrectGuess()
    {
        myClient.Send(MsgType.UpdateVars, new StringMessage("Success"));
    }

    private void GameWon(NetworkMessage netMsg)
    {
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
