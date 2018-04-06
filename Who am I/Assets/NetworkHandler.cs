using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHandler : MonoBehaviour {

    NetworkClient myClient;

    public void SetupClient() {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        //myClient.RegisterHandler(ConnectionMessage, OnScore);
        myClient.Connect("127.0.0.1", 4444);
    }

    private void OnConnected(NetworkMessage netMsg) {
        throw new NotImplementedException();
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
