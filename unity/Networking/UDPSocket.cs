using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System;


public class UDPSocket {

    private UdpClient client = null;
    private IPEndPoint serverEndPoint;
    private List<string> packetBuffer = new List<string>();

    public UDPSocket() { }
    public UDPSocket(string ip, int port) 
    {
        Connect(ip, port);
    }

    public void Connect(string ip, int port) 
    {
        if(client != null && client.Client.Connected) return;
        serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        client = new UdpClient();
        client.Connect(serverEndPoint);
        client.Client.Blocking = false;
        client.BeginReceive(new AsyncCallback(Receive), client);
    }
    public void Send(byte[] dgram)
    {
        if(client != null && client.Client.Connected) 
        {
            client.Send(dgram, dgram.Length);
        }
        else Debug.Log("Client not connected.");
    }
    public void Receive(IAsyncResult result)
    {
        try
        {
            byte [] received = client.EndReceive(result, ref serverEndPoint);
            string packet = System.Text.Encoding.UTF8.GetString(received);
            AddToBuffer(packet);
            client.BeginReceive(new AsyncCallback(Receive), client);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            client.BeginReceive(new AsyncCallback(Receive), client);
        }
    }
    public void Disconnect()
    {
        if(client != null) 
        {
            client.Close();
            client = null;
        }
        packetBuffer.Clear();
    }
    private void AddToBuffer(string packet)
    {
        packetBuffer.Add(packet);
    }
    public List<string> GetBuffer() 
    {
        return packetBuffer;
    }
}
