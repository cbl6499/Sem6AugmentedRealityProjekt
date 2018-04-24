
using System;
using UnityEngine;
using UnityEngine.Networking;

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
>>>>>>> 220dd66f7dec4fba4ac4a70dcfb92e6fcf04a331

        this.Connect();
    }

    private void OnSuccessfulConnection(NetworkMessage netMsg) {
        MessageBase msgBase = netMsg.ReadMessage<Notification>();
        Debug.Log("hell yeah " + netMsg.conn.address);
<<<<<<< HEAD
        //throw new NotImplementedException();

=======
        this.SendLobbyRegistration();
>>>>>>> 220dd66f7dec4fba4ac4a70dcfb92e6fcf04a331
    }

    public void Connect() {
        Debug.Log("I am connecting");
        this.MyClient.Connect(this.HostAddress, port);
    }

    public void SendLobbyRegistration() {
        this.MyClient.Send(MsgType.AddPlayer, new Notification(this.Username));
    }

    public void SendReadyMessage() {
        myClient.Send(MsgType.Ready, new Notification("Ready"));
    }

    private void OnReady(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        Debug.Log("Yay, everyone is ready");
    }

    private void PrintPlayerList(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        Debug.Log("Yay, " + msg.Message + " are in the lobby");
    }

    private Notification CreateConnectionMessage(string username, string userIp) {
        return new Notification(username);
    }

    private void SendGuess(string guess)
    {
        Notification msg = new Notification();
        msg.Message = guess;
        myClient.Send(MsgType.UpdateVars, msg);
    }

    private void GameWon(NetworkMessage netMsg)
    {
        Notification msg = netMsg.ReadMessage<Notification>();
        Debug.Log("Guess was: " + msg.Message);
    }
}
