using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

public class Server : MonoBehaviour {

    public int port = 6321;

    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;

    private TcpListener server;
    private bool serverStarted;

    public void Init() {
        DontDestroyOnLoad(gameObject);
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        startListening();
        serverStarted = true;

        try {
            server = new TcpListener(IPAddress.Any, port);
        } catch (Exception ex) {
            Debug.Log("Socket error: " + ex.Message);
        }
    }

    public void Update() {
        if(!serverStarted) {
            return;
        }

        foreach (ServerClient c in clients) {
            // Is the client still connected

            if(!IsConnected(c.tcp)) {
                c.tcp.Close();
                disconnectList.Add(c);
                continue;
            } else {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable) {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if(data != null) {
                        OnIncomingData(c, data);
                    }
                }
            }
        }

        for(int i = 0; i <disconnectList.Count -1; i++) {

            // Inform Players that they are disconnected

            clients.Remove(disconnectList[i]);
            disconnectList.RemoveAt(i);
        }
    }

    private void Broadcast(string data, List<ServerClient> cl) {
        foreach(ServerClient sc in cl) {
            try {
                StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            } catch (Exception e) {
                Debug.Log("Write error: " + e.Message);
            }
        }
    }

    private void OnIncomingData(ServerClient c, string data) {
        Debug.Log(c.clientName + " : " + data);
    }

    private bool IsConnected(TcpClient c) {
        try {
            if (c != null && c.Client != null && c.Client.Connected) {
                if (c.Client.Poll(0, SelectMode.SelectRead)) {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            } else {
                return false;
            }
        } catch {
            return false;
        }
    }

    private void startListening() {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private void AcceptTcpClient(IAsyncResult ar) {
        TcpListener listener = (TcpListener) ar.AsyncState;

        ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
        clients.Add(sc);

        Debug.Log("Somebody has connected!");

        startListening();
    }
}

public class ServerClient {
    public string clientName;
    public TcpClient tcp;

    public ServerClient(TcpClient tcp) {
        this.tcp = tcp;
    }
}