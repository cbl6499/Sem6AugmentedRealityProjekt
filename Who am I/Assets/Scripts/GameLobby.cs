using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobby {// : MonoBehaviour {

    private List<Player> players;
    private Player owner;
    private int currentPlayerCount;
    private int size;
    private static GameLobby instance;
    private int playersFinished;
   

    public Player Owner{ get; set; }

    public List<Player> Players{ get; set; }

    public int Size { get; set; }

    public static GameLobby Instance{
        get {
            if(instance == null) {
                instance = new GameLobby();
            }
            return instance;
        }
        set {
            instance = value;
        }
    }

    public int CurrentPlayerCount { get; set; }

    private GameLobby() {
        this.CurrentPlayerCount = 0;
        this.Owner = CreatePlayer(Network.player.ipAddress);
        this.Players = new List<Player>();
        this.Players.Add(this.Owner);
        this.playersFinished = 0;
    }

    public int PlayersFinished { get; set; }

    public void SetOwner(string username) {
        this.Owner = CreatePlayer(username);
        if (!this.Players.Contains(this.Owner)) {
            this.Players.Add(Owner);
        }
    }

    public void RegisterPlayer(Player player) {
        Debug.Log("Registered player: " + player.Username);
        players.Add(player);
    }

    public Player CreatePlayer(string username) {
        this.CurrentPlayerCount += 1;
        return new Player(this.CurrentPlayerCount, username);
    }

    public void RemovePlayer(Player player) {
        players.Remove(player);
        this.CurrentPlayerCount -= 1;
    }
    
    /*
    public void StartGame() {
        NetworkHandler handler = NetworkHandler.Instance;
        handler.BroadCastReady();
    }
    */

    private Player GetPlayerById(int id) {
        foreach (Player p in players)
        {
            if (p.Number == id)
            {
                return p;
            }
        }
        return null;
    }
}
