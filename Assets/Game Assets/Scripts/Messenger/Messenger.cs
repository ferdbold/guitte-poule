using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using Message;
using System.Text;
using System.Timers;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;


public class Messenger
{
    private Socket
        Sock;

    public Listener
        listener;


    private string _ip = "192.168.2.165";
    public string ip { get { return this._ip; } set { this._ip = value; } }

    private int
        s_port = 80;


    private bool
        _connectedToServer = false,
        _versionValid = true,
        _versionTested = false,
        _worldConnected = false;

    public Messenger()
    {
        //connect_kolNet();
    }

    public bool ConnectedToServer
    {
        get
        {
            return this._connectedToServer;
        }
        set
        {
            this._connectedToServer = value;
        }
    }

    public bool VersionValid
    {
        get
        {
            return this._versionValid;
        }
        set
        {
            this._versionValid = value;
        }
    }

    private List<message>
        incMessages = new List<message>();

    


    public void Disconnect()
    {
        SendMessage(new message("disconnect"));
        try
        {
            if (Sock != null && Sock.Connected)
            {
                Sock.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }


    public void Connect()
    {
        try
        {

            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Sock.BeginConnect(new IPEndPoint(IPAddress.Parse(_ip), s_port), new AsyncCallback(ConnectCallBack), null);
            Listener listener = new Listener(Sock, this);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }


    private void ConnectCallBack(IAsyncResult AR)
    {

        try
        {

            Sock.EndConnect(AR);
            OnConnect();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }


    private void OnConnect()
    {
        ConnectedToServer = true;
        ValidateVersion();
    }

    private void ValidateVersion()
    {
        SendMessage(new message("Validate"));
    }

    public void SendMessage(message mes)
    {
        try
        {
            Socket mesSock = Sock;

            if (ConnectedToServer)
            {

                try
                {
                    MemoryStream ms = new MemoryStream();
                    
                    byte[] _buffer = conversionTools.convertMessageToBytes(mes);
                    byte[] lenght = BitConverter.GetBytes(_buffer.Length);
                    ms.Write(lenght, 0, lenght.Length);
                    ms.Write(_buffer, 0, _buffer.Length);
                    
                    ms.Close();

                    byte[] data = ms.ToArray();
                    ms.Dispose();
                    Debug.Log(data.Length);
                    mesSock.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), null);

                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            // log errors
        }

        
    }



    public void ThreadSendMessage()
    {
        
    }



    private void SendCallBack(IAsyncResult AR)
    {
        Sock.EndSend(AR);
    }

    public void AddMessageToQue(message incMes)
    {
        incMessages.Add(incMes);
    }

    public void RefreshLoop()
    {
        if (incMessages.Count > 0)
        {
            DoMessages();
        }
    }

    private void DoMessages()
    {
        List<message> completedMessages = new List<message>();
        for (int i = 0; i < incMessages.Count; i++)
        {
            try
            {
                NetManager.instance.HandleMessage(incMessages[i]);
                completedMessages.Add(incMessages[i]);
            }
            catch { }
        }
        for (int i = 0; i < completedMessages.Count; i++)
        {
            try
            {
                incMessages.Remove(completedMessages[i]);
            }
            catch { }
        }
    }


}
