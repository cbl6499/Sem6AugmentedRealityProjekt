﻿
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WhoAmIClient : NetworkBehaviour {

    NetworkClient myClient;
    private string hostAddress;
    private string username;
    private int port = 63210;
    private static WhoAmIClient instance;
    private int localClient;
    private List<Player> playerList = new List<Player>();
    private bool readyToGo = false;

    private short owner = 333;
    private short updateVars = 332;
    private short spawnFinished = 331;
    private short syncList = 330;
    private short connect = 329;
    private short addPlayer = 328;
    private short ready = 327;
    private short lobbyReadyToBegin = 326;
    private short playerGuessedRight = 325;
    private short resultMessage = 324;
    private short faceAssigned = 323;
    private short gameFinished = 322;
    private short currentPoints = 321;
    private short restartLobby = 320; 
    private short guess = 319;
    private short resetlobby = 318;
    private short closeLobby = 317;
    private short setguessactive = 316;

    public int Port { get; set; }
    public string HostAddress { get; set; }
    public string Username { get; set; }

    public List<Player> PlayerList { get; set; }
    public bool ReadyToGo { get; set; }
    public NetworkClient MyClient { get; set; }
    public static WhoAmIClient Instance {
        get {
            if (instance == null) {
                instance = new WhoAmIClient();
               // instance.SetupClient();
            }
            return instance;
        }
        set {
            instance = value;
        }
    }



    public void SetupClient() {
        this.MyClient = new NetworkClient();
        this.MyClient.RegisterHandler(ready, OnReady);
        this.MyClient.RegisterHandler(syncList, SetPlayerList);
        this.MyClient.RegisterHandler(connect, OnSuccessfulConnection);
        this.MyClient.RegisterHandler(spawnFinished, SetLocalClient);
        this.MyClient.RegisterHandler(playerGuessedRight, GameWon);
        this.MyClient.RegisterHandler(lobbyReadyToBegin, LobbyReady);
        this.MyClient.RegisterHandler(faceAssigned, AssignFaceToPlayer);
        this.MyClient.RegisterHandler(gameFinished, FinishGame);
        this.MyClient.RegisterHandler(currentPoints, GetCurrentPointTable);
        this.MyClient.RegisterHandler(restartLobby, RestartLobby);
        this.MyClient.RegisterHandler(closeLobby, CloseLobby);
        this.MyClient.RegisterHandler(setguessactive, SetGuessActive);
    }

    public void CloseLobby(NetworkMessage netMsg) {
        GameManager.Instance.CloseLobby();
    }

    public void GetCurrentPointsFromServer() {
        this.MyClient.Send(currentPoints, new StringMessage("give me points"));
    }
    public void GetCurrentPointTable(NetworkMessage netMsg) {
        string points = netMsg.ReadMessage<StringMessage>().value;
        Debug.Log("Points: " + points);
        GameManager.Instance.SetCurrentPoints(points);
    }

    public void RestartLobby(NetworkMessage netMsg) {
        GameManager.Instance.RestartGame();
        
    }

    public void SendSetGuessActive() {
        this.MyClient.Send(setguessactive, new StringMessage("Party"));
    }

    public void SetGuessActive(NetworkMessage netMsg) {
        GameManager.Instance.setGuessActive();
    }

    public void SendRestartLobbyToServer() {
        this.MyClient.Send(restartLobby, new StringMessage("Start over"));
    }
    public void FinishGame(NetworkMessage netMsg) {
        GameManager.Instance.FinishGame();
    }

    public void AssignFaceToPlayer(NetworkMessage netMsg) {
        string msg = netMsg.ReadMessage<StringMessage>().value;
        string[] data = msg.Split('|');
        Debug.Log(data[0] + " | " + data[1]);
        Player player = findPlayerById(Int32.Parse(data[0]));
        if (player != null) {
            Debug.Log(player.Username + " gets: " + data[1]);
            player.Face = data[1];
            GameManager.Instance.SetFaceForPerson(player);
        } else {
            Debug.Log("Player not found");
        }
    }

    private Player findPlayerById(int id) {
        foreach (Player player in playerList) {
            if (player.Number == id) {
                return player;
            }
        }
        return null;
    }

    private void LobbyReady(NetworkMessage netMsg) {
        Debug.Log("Lobby is Ready to begin!");
        GameManager.Instance.StartGame();
        readyToGo = true;
    }

    private void OnSuccessfulConnection(NetworkMessage netMsg) {
        this.SendLobbyRegistration();
    }

    public void Connect() {
        Debug.Log("I am connecting");
        this.MyClient.Connect(this.HostAddress, port);
    }

    public void SendLobbyRegistration() {
        this.MyClient.Send(addPlayer, new StringMessage(this.Username));
    }

    public void SendReadyMessage() {
        myClient.Send(ready, new StringMessage("Ready"));
    }

    private void OnReady(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Yay, everyone is ready");
    }

    private void SetLocalClient(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        this.localClient = Int32.Parse(msg.value);
        GameManager.Instance.SetPlayerNumber(this.localClient);
    }

    private void SetPlayerList(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Yay, " + msg.value + " are in the lobby");
        string[] array = msg.value.Split(',');
        foreach (string a in array) {
            string[] player = a.Split('|');
            player[0].Trim();
            if (a != "") {
                Player tempPlayer = new Player(Int32.Parse(player[0]), player[1]);
                bool alreadyExisting = false;
                foreach (Player p in playerList) {
                    if (p.Equals(tempPlayer)) {
                        alreadyExisting = true;
                    }
                }
                Debug.Log(player[0]);
                if (!alreadyExisting) {
                    playerList.Add(tempPlayer);
                }
            }
        }
    }

    private StringMessage CreateConnectionMessage(string username, string userIp) {
        return new StringMessage(username);
    }

    private void SendCorrectGuess() {
        this.MyClient.Send(guess, new StringMessage(localClient.ToString()));
    }

    private void GameWon(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log("Guess was: " + msg.value);
    }

    public void SendCloseLobbyCommand() {
        this.MyClient.Send(closeLobby, new StringMessage("End"));
    }

    public bool CheckGuess(string face) {
        Debug.Log(face);
        string guess = playerList[localClient].Face;
        guess.Trim();
        string newguess = "";
        char[] guessarray = face.ToCharArray();

        for(int i = 0; i < guessarray.Length; i++) {
            if(guessarray[i] != ' ') {
                newguess += guessarray[i];
            }
        }

        guess.ToLower();
        if (newguess.ToLower() == guess.ToLower()) {
            SendCorrectGuess();
            Debug.Log("Guess was Correct");
            return true;
        }
        Debug.Log("The Guess was not correct!");
        return false;
    }

    public void SendFaceSelectionToServer(int id, string face) {
        Debug.Log(id + " | " + face);
        if(this.MyClient == null) {
            this.SetupClient();
            this.Connect();
            
            Debug.Log("test" + this.MyClient.hostPort);
        }
        Debug.Log(this.MyClient.hostPort);

        this.MyClient.Send(faceAssigned, new StringMessage(id + "|" + face));
    }

    public bool AllFacesSet() {
        foreach(Player player in playerList) {
            if (!player.FaceSet) {
                return false;
            }
        }
        return true;
    }

    public void ResetPlayers() {
        foreach (Player player in playerList) {
            player.FaceSet = false;
        }
    }

}
