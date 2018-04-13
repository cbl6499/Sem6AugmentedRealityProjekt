using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Notification : MessageBase {

    private string message;
    private int ip;

    public string Message { get; set; }

    public string Ip{ get; set; }
    
    
    // Use this for initialization
    public Notification() { }
    public Notification(string message, string ip) {
        this.Message = message;
        this.Ip = ip;
    }
}
