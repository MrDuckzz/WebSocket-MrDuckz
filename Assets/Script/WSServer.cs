using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;

namespace testWS
{
    public class Laputa : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            // string data = "";
            Debug.Log(e.Data);
            // Send(e.Data);
            if (e.Data == "Ping")
            {
                Send("Pong");
            }
            Sessions.Broadcast(e.Data);

        }
    }

    public class WSServer : MonoBehaviour
    {
        WebSocketServer wssv;

        private void OnDestroy()
        {
            wssv.Stop();
            Debug.Log("Stop");
        }

        public void Start()
        {
            wssv = new WebSocketServer("ws://localhost:4610");

            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.Start();

            Debug.Log(wssv.IsListening);
            // Console.ReadKey(true);
            // wssv.Stop();
        }
        public void testInvoke()
        {

        }
    }
}