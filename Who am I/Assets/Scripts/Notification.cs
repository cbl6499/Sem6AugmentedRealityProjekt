using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Notification : MessageBase {

    private string message;

    public string Message { get; set; }
    
    public Notification() {

    }
    public Notification(string message) {
        this.Message = message;
    }
}
