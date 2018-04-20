
using UnityEngine;
using UnityEngine.Networking;

public class WhoAmIClient : NetworkBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    //private string clientAddress;
    private string username;
    private int port = 63210;
    private static WhoAmIClient instance;

    public int Port { get; set; }
    public string HostAddress { get; set; }
  //  public string ClientAddress { get; set; }
    public string Username { get; set; }

    void Start() {
     //   SetupClient();
    }

    void Update() {

    }
    /*
    public WhoAmIClient() {
       // this.clientAddress = address;
        SetupClient();
        
    }
    */
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

    //Client Methode
    public void SetupClient() {
       // Debug.Log("SetupClient");
        this.MyClient = new NetworkClient();
      //  Debug.Log("new NetworkClient()");
        this.MyClient.RegisterHandler(MsgType.Ready, OnReady);
       // Debug.Log("Register OnReady");
        this.MyClient.RegisterHandler(MsgType.NotReady, PrintPlayerList);
        // Debug.Log("Register PrintPlayerList");
        //Debug.Log(this.HostAddress);
        this.Connect();
        //Debug.Log("Connect to Server");
    }

    public void Connect() {
        //Network.Connect(hostAddress, port);
        this.MyClient.Connect(this.HostAddress, port);
    }

    //Client Methode
    public void SendLobbyRegistration() {
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        Debug.Log(Network.player.ipAddress);
        msg.Message = this.Username;
        Debug.Log("Notification created");
        myClient.Send(MsgType.AddPlayer, msg);
    }

    //Client Methode
    public void SendReadyMessage(Player p) {
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        msg.Message = "Ready";
        myClient.Send(MsgType.Ready, msg);
    }

    //Client Methode (Listener)
    private void OnReady(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        //do stuff and start game
        Debug.Log("Yay, everyone is ready");
    }

    private void PrintPlayerList(NetworkMessage netMsg) {
        Notification msg = netMsg.ReadMessage<Notification>();
        Debug.Log("Yay, " + msg.Message + " are in the lobby");
    }

    //Client Methode
    private Notification CreateConnectionMessage(string username, string userIp) {
        return new Notification(username, userIp);
    }
}
