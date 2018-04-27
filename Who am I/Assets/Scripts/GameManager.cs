using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    Dictionary<String, int> persons;

    public static GameManager Instance { set; get; }

    private WhoAmIClient client;
    private WhoAmIServer server;
    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;
    public GameObject hostSettingMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    string username;

    void Start() {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        hostSettingMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
        
        this.client = WhoAmIClient.Instance;
        this.server = WhoAmIServer.Instance;
        DontDestroyOnLoad(client);
        DontDestroyOnLoad(server);
        DontDestroyOnLoad(serverPrefab);
        DontDestroyOnLoad(clientPrefab);
        persons = new Dictionary<string, int>();
        persons.Add("Person1Selection", 1);
        persons.Add("Person2Selection", 2);
        persons.Add("Person3Selection", 3);
        persons.Add("Person4Selection", 4);
        persons.Add("Person5Selection", 5);
        persons.Add("Person6Selection", 6);

    }

    public void ConnectButton() {
        username = GameObject.Find("Username").GetComponent<InputField>().text;
        if (username == "") {
            GameManager.print("Username was empty");
            username = "testUser";
        } else {
            mainMenu.SetActive(false);
            connectMenu.SetActive(true);
        }
    }

    public void HostButton() {
        username = GameObject.Find("Username").GetComponent<InputField>().text;
        Debug.Log(username);
        if (username == "") {
            GameManager.print("Username was empty");
            username = "testUser";
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
            client.Username = username;
            client.HostAddress = "127.0.0.1";
            server.SetupHost(amount);
            client.SetupClient();
            client.Connect();
            Debug.Log("I made it to send method " + client.Username + " " + client.HostAddress);

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
            client.Username = username;
            client.HostAddress = hostAddress;
            Debug.Log("I mad it to to send method " + username + " " + hostAddress + " Client:" + client.Username + " " + client.HostAddress);
            client.SetupClient();
            client.Connect();
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

    public void StartGame() {
        Debug.Log("sceneName to load: 2D_Scene");
        SceneManager.LoadScene("2D_Scene");
    }

    public void GuessButtonClick() {
        string guess = GameObject.Find("GuessInput").GetComponent<InputField>().text;
        
        if (client.CheckGuess(guess)) {
            Debug.Log("GG EZ");
        } else {
            Debug.Log("LOL NOOB");
        }
    }

    public void FixSelection(GameObject selection, GameObject player) {
        int id = persons[player.name];
        Debug.Log(selection.name);
        Debug.Log(player.name);
        Debug.Log(id+"");
        client.SendFaceSelectionToServer(id, selection.name);
        
    }

    public void SetFaceForPerson(Player player) {
        string personName = "";
        foreach(string name in persons.Keys) {
            if(persons[name] == player.Number) {
                personName = name;
            }
        }
        GameObject person = GameObject.Find(personName);
        foreach(Transform child in transform) {
            Debug.Log(child.name);
        }
        
    }

    public void BackButton() {
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        hostSettingMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
