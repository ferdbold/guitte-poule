using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Message;
using System.IO;

public class Listener
{
    public Socket sock;
    public string type_svr;
    private Messenger mesenger;
    private int MAX_INC_Data = 512000;

    public Listener(Socket s, Messenger mesenger_ref)
    {
        sock = s;
        mesenger = mesenger_ref;
        sock.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
    }

    void callback(IAsyncResult AR)
    {
        sock.ReceiveBufferSize = 512;
        try
        {
            sock.EndReceive(AR);

            while (sock.Connected)
            {
                byte[] sizeInfo = new byte[4];

                int bytesRead = 0, currentRead = 0;

                currentRead = bytesRead = sock.Receive(sizeInfo);

                while (bytesRead < sizeInfo.Length && currentRead > 0)
                {
                    currentRead = sock.Receive(sizeInfo, bytesRead, sizeInfo.Length - bytesRead, SocketFlags.None);
                    bytesRead += currentRead;
                }

                int messageSize = BitConverter.ToInt32(sizeInfo, 0);
                byte[] incMessage = new byte[messageSize];

                bytesRead = 0;
                currentRead = bytesRead = sock.Receive(incMessage, bytesRead, incMessage.Length - bytesRead, SocketFlags.None);

                while (bytesRead < messageSize && currentRead > 0)
                {
                    currentRead = sock.Receive(incMessage, bytesRead, incMessage.Length - bytesRead, SocketFlags.None);
                    bytesRead += currentRead;
                }

                try
                {
                    if (incMessage != null)
                    {
                        mesenger.AddMessageToQue(conversionTools.convertBytesToMessage(incMessage));
                    }
                }
                catch { }
            }



            sock.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }
        catch (Exception ex)
        {

            /*if (type_svr == "login")
            {
                mesenger.connectedToServer = false;
            }*/
            sock.Close();
        }
    }
}
