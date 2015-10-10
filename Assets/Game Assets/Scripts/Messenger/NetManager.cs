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
using Tendresse.Data;

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


    void Start()
    {
        Application.runInBackground = true;
        messenger.Connect();
        message mes = new message("Validate");



    }

    void Update()
    {
        messenger.RefreshLoop();
    }
  
    public void HandleMessage(message mes)
    {
       // Debug.Log(message.id);
        switch (mes.messageText)
        {
            /*--------------------------------------  Movement Update   -----------------------------------------*/
            case "startMatch":
                GameManager.instance.Event_OnFindPartner();
                break;
            case "receiveImage":
                GameManager.instance.Event_OnReceiveImage(MakeImageFromMessage(mes));
                break;
    
            /*----------------------------------------------------------------------------------------------------*/
            default:
                Debug.Log("The server has sent the message: " + mes.messageText);
                Debug.Log(conversionTools.convertMessageToString(mes));
                break;
        }
    }


    public message MakeMessageFromImage(TendresseData image)
    {
        message img = new message("sendImage");
        
        for (int i = 0; i < image.pointList.Count; i++)
        {
            NetObject lineObj = new NetObject("");
            List<Vector3> line = image.pointList[i];
            for (int j = 0; j < line.Count; j++)
            {
                lineObj.addFloat("", line[j].x);
                lineObj.addFloat("", line[j].y);
            }
            img.addNetObject(lineObj);
        }
        return img;
    }

    public TendresseData MakeImageFromMessage(message image)
    {
        TendresseData img = new TendresseData();

        for (int i = 0; i < image.getNetObjectCount(); i++)
        {
            NetObject lineObj = image.getNetObject(i);
            List<Vector3> line = new List<Vector3>();
            for (int j = 0; j < lineObj.getFloatCount(); j+=2)
            {
                line.Add(new Vector3(lineObj.getFloat(j), lineObj.getFloat(j+1), 0));
            }
            img.pointList.Add(line);
        }
        return img;
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