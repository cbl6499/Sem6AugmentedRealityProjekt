
using UnityEngine;
using UnityEngine.Networking;

public class WhoAmIClient : MonoBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private string clientAddress;
    private string username;
    private int port = 6321;
    private WhoAmIClient instance;

    public int Port { get; set; }
    public string HostAddress { get; set; }
    public int ClientAddress { get; set; }
    public int Username { get; set; }

    void Start() {
        SetupClient();
    }

    private WhoAmIClient(string address) {
        this.clientAddress = address;
    }

    public WhoAmIClient Instance {
        get {
            if (instance == null) {
                instance = new WhoAmIClient(Network.player.ipAddress);
            }
            return instance;
        }
        set {
            instance = value;
        }
    }

    //Client Methode
    public void SetupClient() {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Ready, OnReady);
        myClient.RegisterHandler(MsgType.NotReady, PrintPlayerList);
        myClient.Connect(this.HostAddress, port);
    }

    //Client Methode
    public void SendLobbyRegistration() {
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        msg.Message = username;
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
