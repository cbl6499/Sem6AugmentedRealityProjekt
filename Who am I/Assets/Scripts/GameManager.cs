using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { set; get; }

    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;
    public GameObject hostSettingMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;
    
    string username;

    void Start () {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        hostSettingMenu.SetActive(false);
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
            mainMenu.SetActive(false);
            hostSettingMenu.SetActive(true);
        }        
    }

    public void CreateGameButton() {
        int amount = Convert.ToInt32(GameObject.Find("PlayerAmount").GetComponent<InputField>().text);
        if (amount == null) {
            amount = 4;
        } else if (amount > 6) {
            GameManager.print("Lobby would be to big!");
            GameObject.Find("PlayerAmount").GetComponent<InputField>().text = "6";
        } else {
            WhoAmIServer s = WhoAmIServer.Instance; //Instantiate(serverPrefab.GetComponent<WhoAmIServer>());
            WhoAmIClient c = WhoAmIClient.Instance;//Instantiate(clientPrefab.GetComponent<WhoAmIClient>());
            c.Username = username;
            //c.ClientAddress = "127.0.0.1";
            c.HostAddress = "127.0.0.1";
            s.SetupHost();
            c.SetupClient();
            
            serverMenu.SetActive(true);
            hostSettingMenu.SetActive(false);
        }
    }

    public void ConnectToHostButton() {
        string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if (hostAddress == "") {
            hostAddress = "127.0.0.1";
        }
        try {
            WhoAmIClient c = WhoAmIClient.Instance;// Instantiate(clientPrefab.GetComponent<WhoAmIClient>());
            c.Username = username;
            c.HostAddress = hostAddress;
            Debug.Log("I mad it to to send method " + username + " " + hostAddress + " Client:" + c.Username + " " + c.HostAddress );
            c.SetupClient();
            //c.Connect();
            c.SendLobbyRegistration();
        } catch (Exception e) {
            //Debug.Log(e.Message);
        }
    }

    public void OnGameStart(){
        Debug.Log("sceneName to load: 2D_Scene");
        SceneManager.LoadScene("2D_Scene");
    }

    public void GuessButtonClick() {
        string guess = GameObject.Find("GuessInput").GetComponent<InputField>().text;
        WhoAmIClient client = WhoAmIClient.Instance;
        client.SendGuess(guess);
    }
	
    public void BackButton() {
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        hostSettingMenu.SetActive(false);
        mainMenu.SetActive(true);
    }


}
