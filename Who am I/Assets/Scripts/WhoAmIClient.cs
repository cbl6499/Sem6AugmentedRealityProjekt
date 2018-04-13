
using UnityEngine;
using UnityEngine.Networking;

public class WhoAmIClient : MonoBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private int port;

    public int Port { get; set; }
    public string HostAddress { get; set; }

    private void Start() {
        SetupClient();
    }

    //Client Methode
    public void SetupClient() {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Ready, OnReady);
        myClient.Connect(this.HostAddress, port);
    }

    //Client Methode
    public void SendLobbyRegistration(string username) {
        Notification msg = new Notification();
        msg.Ip = Network.player.ipAddress;
        msg.Message = username;
        myClient.Send(MsgType.AddPlayer, msg);
    }

    //Client Methode
    public void SendReadyMessage(Player p) {
        myClient.Connect(p.Ip, port);
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

    //Client Methode
    private Notification CreateConnectionMessage(string username, string userIp) {
        return new Notification(username, userIp);
    }
}
