using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using Message;
using System.Security.Cryptography;
using System.Text;
using System.Timers;

public class NetManager : MonoBehaviour
{
    private Messenger messenger = new Messenger();



    private static NetManager self;
    public static NetManager instance
    {
        get
        {
            return self;
        }
    }

    void Awake()
    {
        self = this;
        DontDestroyOnLoad(transform.gameObject);
    }


    void Update()
    {
        messenger.RefreshLoop();
    }

    void Start()
    {
        Application.runInBackground = true;
        messenger.Connect();
        message mes = new message("Validate");



    }
  
    public void HandleMessage(message mes)
    {
        switch (mes.messageText)
        {
            /*--------------------------------------     -----------------------------------------*/
            case "":
                
                break;
    
            /*----------------------------------------------------------------------------------------------------*/
            default:
                Debug.Log("The server has sent the message: " + mes.messageText);
                Debug.Log(conversionTools.convertMessageToString(mes));
                break;
        }
    }


    /*---------------------------------------------------------------------------------*
     * ------------------------------   Envoi  Messages   -----------------------------*
     * --------------------------------------------------------------------------------*/

    public void SendMessage(message mes)
    {
        messenger.SendMessage(mes);
    }


    /*---------------------------------------------------------------------------------*
    * ------------------------------   Fermeture jeu   -----------------------------*
    * --------------------------------------------------------------------------------*/
 
    private void OnApplicationQuit()
    {
        messenger.Disconnect();
    }



}