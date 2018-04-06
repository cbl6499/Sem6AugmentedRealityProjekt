using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectionMessage : MessageBase {

    private string user;
    private int ip;


    public string User { get; set; }
    public string Ip{ get; set; }
    
    
    // Use this for initialization
    public ConnectionMessage() { }
    public ConnectionMessage(string user, string ip) {
        this.User = user;
        this.Ip = ip;
    }
}
