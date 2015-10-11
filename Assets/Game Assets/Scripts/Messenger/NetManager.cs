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
    private Messenger messenger;

   
    static public NetManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            messenger = new Messenger();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }


    void Start()
    {
        Application.runInBackground = true;
        //#if !UNITY_EDITOR
        messenger.Connect();
        //#endif
        message mes = new message("Validate");

    }

    void Update()
    {
        messenger.RefreshLoop();
    }
  
    public void HandleMessage(message mes)
    {
        Debug.Log(mes.messageText);
        switch (mes.messageText)
        {
            /*--------------------------------------  Movement Update   -----------------------------------------*/
            case "startEvent":
                DateManager.instance.OnStartNewEvent(mes.getNetObject(0).getString(0), mes.getNetObject(0).getInt(0) == 0);
                break;
            case "receiveImage":
                GameManager.instance.Event_OnReceiveImage(MakeImageFromMessage(mes));
                break;
            case "receiveTexte":
                DateManager.instance.ExecuteDateEvent_OnReceiveText();
                break;

            case "receiveSound": //TODO reconnect this
                AudioManager.instance.PlaySoundFromMessage(mes);
                break;
            
            case "startMatch":
                GameManager.instance.Event_OnFindPartner(mes.getNetObject(0).getBool(0));
                break;
           
            case "startDate":
                DateManager.instance.OnStartNewDate(mes.getNetObject(0).getString(0),mes.getNetObject(0).getString(1),mes.getNetObject(0).getInt(0), mes.getNetObject(0).getInt(2));
                break;

            case "oponenName":
                DateManager.instance.OnGetOwnName(mes.getNetObject(0).getString(0));
                break;

            case "endDate":
                
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
        List<List<Vector3>> points = new List<List<Vector3>>();
        for (int i = 0; i < image.getNetObjectCount(); i++)
        {
            NetObject lineObj = image.getNetObject(i);
            List<Vector3> line = new List<Vector3>();
            for (int j = 0; j < lineObj.getFloatCount(); j+=2)
            {
                line.Add(new Vector3(lineObj.getFloat(j), lineObj.getFloat(j+1), 0));
            }
            points.Add(line);
        }
        return new TendresseData(points);;
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