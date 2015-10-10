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
            case "":
                
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
        message img = new message("image");
        img.addNetObject(new NetObject("x"));
        img.addNetObject(new NetObject("y"));
        for (int i = 0; i < image.pointList.Count; i++)
        {
             img.getNetObject(0).addFloat("",image.pointList[i][0].x);
             img.getNetObject(1).addFloat("",image.pointList[i][0].y);
        }
        return img;
    }

    public TendresseData MakeImageFromMessage(message image)
    {
        TendresseData img = new TendresseData();
        NetObject pointX = image.getNetObject(0);
        NetObject pointY = image.getNetObject(1);

        for(int i=0; i < pointX.getFloatCount();i+=2)
        {
            List<Vector3> line = new List<Vector3>();
            line.Add(new Vector3(pointX.getFloat(i), pointY.getFloat(i), 0));
            line.Add(new Vector3(pointX.getFloat(i+1), pointY.getFloat(i+1), 0));
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