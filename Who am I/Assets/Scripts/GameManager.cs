using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    Dictionary<String, int> persons;
    Dictionary<String, String> loadPersons;
    public static GameManager Instance { set; get; }

    private WhoAmIClient client;
    private WhoAmIServer server;
    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;
    public GameObject hostSettingMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    public Text playerNumber;
    public Text playerAmount;

    public GameObject player_1;
    public GameObject player_2;
    public GameObject player_3;
    public GameObject player_4;
    public GameObject player_5;
    public GameObject player_6;

    public GameObject scoreboard;
    public GameObject restart;
    public GameObject lobby;
	public GameObject subButton;
	public GameObject textField;

	public Text guessAnswer;

    public GameObject guess;

    private int number;
    private int lobbySize;

    public Text guessText;
    public Text score;

    public string currentPoints = "";

    string username;

    public string CurrentPoints { get; set; }
    public int Number { get; set; }
    void Start() {
        Instance = this;

        player_1.SetActive(false);
        player_2.SetActive(false);
        player_3.SetActive(false);
        player_4.SetActive(false);
        player_5.SetActive(false);
        player_6.SetActive(false);

        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        hostSettingMenu.SetActive(false);

        this.client = WhoAmIClient.Instance;
        this.server = WhoAmIServer.Instance;

        DontDestroyOnLoad(client);
        DontDestroyOnLoad(server);
        DontDestroyOnLoad(gameObject);




         DontDestroyOnLoad(serverPrefab);
         DontDestroyOnLoad(clientPrefab);
        persons = new Dictionary<string, int>();
        persons.Add("Person1Selection", 0);
        persons.Add("Person2Selection", 1);
        persons.Add("Person3Selection", 2);
        persons.Add("Person4Selection", 3);
        persons.Add("Person5Selection", 4);
        persons.Add("Person6Selection", 5);

        loadPersons = new Dictionary<string, string>();

       // loadPersons.Add("","");
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
        if (amount == 0) {
            amount = 4;
        } else if (amount > 6) {
            GameManager.print("Lobby would be to big!");
            GameObject.Find("PlayerAmount").GetComponent<InputField>().text = "6";
        } else {
            lobbySize = amount;
            client.Username = username;
            client.HostAddress = "127.0.0.1";
            server.SetupHost(amount);
            client.SetupClient();
            client.Connect();
            playerAmount.gameObject.SetActive(true);
            playerAmount.text = Network.player.ipAddress;
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
            client.SetupClient();
            client.Connect();
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

    public void StartGame() {
        playerAmount.gameObject.SetActive(false);
        connectMenu.SetActive(false);
        serverMenu.SetActive(false);

        //guess.SetActive(false);
        
		guessAnswer.gameObject.SetActive (false);
        player_1.SetActive(true);
        player_2.SetActive(true);
        player_3.SetActive(true);
        player_4.SetActive(true);
        player_5.SetActive(true);
        player_6.SetActive(true);

    }

    public void RestartGameButton() {
        RestartGame();
    }

    public void RestartGame() {
        scoreboard.SetActive(false);
        restart.SetActive(false);
        client.SendRestartLobbyToServer();
		countFaceAssigned = 0;
		player_1.SetActive(true);
		player_2.SetActive(true);
		player_3.SetActive(true);
		player_4.SetActive(true);
		player_5.SetActive(true);
		player_6.SetActive(true);
    }

    public void GuessButtonClick() {
        string guess = this.guessText.text;
		guessAnswer.gameObject.SetActive (true);
        if (client.CheckGuess(guess)) {
			guessAnswer.text = "Damn you are good, you got it right!";
			subButton.SetActive (false);
			textField.SetActive (false);
        } else {
			guessAnswer.text = "Damn that was a bad answer, better luck next time!";
        }
    }

    public void FixSelection(GameObject selection, GameObject player, GameObject selectPlayerList, GameObject selectButton, GameObject infoCanvas) {
        int id = persons[player.name];
        client.SendFaceSelectionToServer(id, selection.name);
    }

    private int countFaceAssigned = 0;

    public void SetFaceForPerson(Player player) {
        string personName = "";
        foreach(string name in persons.Keys) {
            if(persons[name] == player.Number) {
                personName = name;
            }
        }
        switch (player.Number + 1) {
            case 1:
                string player1face = player.Face;
                player_1.transform.Find(player.Face).gameObject.SetActive(true);
                player_1.transform.Find("Canvas").transform.Find("PersonSelect").gameObject.SetActive(false);
                player_1.transform.Find("Canvas").transform.Find("SelectButton").gameObject.SetActive(false);
                player_1.transform.Find("InfoCanvas").gameObject.SetActive(true);
                break;
            case 2:
                string player2face = player.Face;
                player_2.transform.Find(player.Face).gameObject.SetActive(true);
                player_2.transform.Find("Canvas").transform.Find("PersonSelect").gameObject.SetActive(false);
                player_2.transform.Find("Canvas").transform.Find("SelectButton").gameObject.SetActive(false);
                player_2.transform.Find("InfoCanvas").gameObject.SetActive(true);
                break;
            case 3:
                string player3face = player.Face;
                player_3.transform.Find(player.Face).gameObject.SetActive(true);
                player_3.transform.Find("Canvas").transform.Find("PersonSelect").gameObject.SetActive(false);
                player_3.transform.Find("Canvas").transform.Find("SelectButton").gameObject.SetActive(false);
                player_3.transform.Find("InfoCanvas").gameObject.SetActive(true);
                break;
            case 4:
                string player4face = player.Face;
                player_4.transform.Find(player.Face).gameObject.SetActive(true);
                player_4.transform.Find("Canvas").transform.Find("PersonSelect").gameObject.SetActive(false);
                player_4.transform.Find("Canvas").transform.Find("SelectButton").gameObject.SetActive(false);
                player_4.transform.Find("InfoCanvas").gameObject.SetActive(true);
                break;
            case 5:
                string player5face = player.Face;
                player_5.transform.Find(player.Face).gameObject.SetActive(true);
                player_5.transform.Find("Canvas").transform.Find("PersonSelect").gameObject.SetActive(false);
                player_5.transform.Find("Canvas").transform.Find("SelectButton").gameObject.SetActive(false);
                player_5.transform.Find("InfoCanvas").gameObject.SetActive(true);
                break;
            case 6:
                string player6face = player.Face;
                player_6.transform.Find(player.Face).gameObject.SetActive(true);
                player_6.transform.Find("Canvas").transform.Find("PersonSelect").gameObject.SetActive(false);
                player_6.transform.Find("Canvas").transform.Find("SelectButton").gameObject.SetActive(false);
                player_6.transform.Find("InfoCanvas").gameObject.SetActive(true);
                break;
        }
        countFaceAssigned++;

        Debug.Log("Faces assigned: " + countFaceAssigned + ", " + lobbySize);
        if (countFaceAssigned == lobbySize){
            //guess.SetActive(true);
            client.SendSetGuessActive();
        }

    }

    public void setGuessActive() {
        guess.SetActive(true);
    }

    public void CloseLobbyButton() {
        client.SendCloseLobbyCommand();
    }

    public void CloseLobby() {
        GameLobby.Instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /*client = null;
        server = null;
        WhoAmIServer.Instance = null;
        WhoAmIClient.Instance = null;
        client = WhoAmIClient.Instance;
        server = WhoAmIServer.Instance;
        */
    }


    public void FinishGame() {
        player_1.SetActive(false);
        player_2.SetActive(false);
        player_3.SetActive(false);
        player_4.SetActive(false);
        player_5.SetActive(false);
        player_6.SetActive(false);

		guessAnswer.gameObject.SetActive (false);

        scoreboard.SetActive(true);
        guess.SetActive(false);

        client.GetCurrentPointsFromServer();

        score.text = "Waiting for point list...";
        
        Debug.Log("Scoreboard should be here");


        if(this.Number == 1) {
            restart.SetActive(true);
			lobby.SetActive(true);
        }
    }

    public void SetPlayerNumber(int number) {
        number++;
        this.Number = number;
        playerNumber.text = "You are player: " + number;
    }

    public void SetCurrentPoints(string points) {
        score.text = points;
    }


    public void BackButton() {
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        hostSettingMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
