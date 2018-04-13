using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { set; get; }

    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

<<<<<<< HEAD
    string username;

=======
>>>>>>> d6b4e89af04f2c969ee4d3b1eec207a91787831c
    // Use this for initialization
    void Start () {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
	}

    public void ConnectButton() {
        username = GameObject.Find("Username").GetComponent<InputField>().text;
        if (username == "") {
            GameManager.print("Username was empty");
        } else {
            mainMenu.SetActive(false);
            connectMenu.SetActive(true);
        }
    }

    public void HostButton() {
        string username = GameObject.Find("Username").GetComponent<InputField>().text;
        if (username == "") {
            GameManager.print("Username was empty");
        } else {
            WhoAmIServer s = Instantiate(ServerPrefab.GetComponent<WhoAmIServer>());
            WhoAmIClient c = Instantiate(ClientPrefab.GetComponent<WhoAmIClient>());
            c.Username = username;
            c.ClientAddress = "127.0.0.1";
            c.HostAddress = "127.0.0.1";
            mainMenu.SetActive(false);
            serverMenu.SetActive(true);
        }        
    }

    public void ConnectToHostButton() {
        string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if (hostAddress == "") {
            hostAddress = "127.0.0.1";
        }

        try {
            WhoAmIClient c = Instantiate(ClientPrefab.GetComponent<WhoAmIClient>());
            c.Username = username;
            c.HostAddress = hostAddress;
            c.SendLobbyRegistration();
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
    }
	
    public void BackButton() {
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
