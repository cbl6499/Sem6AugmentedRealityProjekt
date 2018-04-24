
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WhoAmIClient : NetworkBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private string username;
    private int port = 63210;
    private static WhoAmIClient instance;

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
<<<<<<< HEAD

        
        this.MyClient.RegisterHandler(MsgType.UpdateVars, GameWon);
        
        // Debug.Log("Register PrintPlayerList");
        //Debug.Log(this.HostAddress);

=======
        
        //this.MyClient.RegisterHandler(MsgType.UpdateVars, GameWon);
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
        this.Connect();
    }

    private void OnSuccessfulConnection(NetworkMessage netMsg) {
<<<<<<< HEAD
        MessageBase msgBase = netMsg.ReadMessage<Notification>();
        Debug.Log("hell yeah " + netMsg.conn.address);

=======
        //Debug.Log("hell yeah " + netMsg.ReadMessage<StringMessage>().value);
        Debug.Log("Connection #läuft");
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
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

<<<<<<< HEAD
    {
        Notification msg = new Notification();
        msg.Message = guess;
=======
    public void SendGuess(string guess) {
        StringMessage msg = new StringMessage();
        msg.value = guess;
>>>>>>> 0f8d865f25cdc1154110d62d3ae44f4639d922f6
        myClient.Send(MsgType.UpdateVars, msg);
    }

    private void GameWon(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Guess was: " + msg.value);
    }
}
