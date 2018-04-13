using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { set; get; }

    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    // Use this for initialization
    void Start () {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
	}

    public void ConnectButton() {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    }

    public void HostButton() {
        mainMenu.SetActive(false);
        serverMenu.SetActive(true);
    }

    public void ConnectToHostButton() {

    }
	
    public void BackButton() {
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
