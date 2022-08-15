using System.Net.Mime;
using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Net;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class WSClient : MonoBehaviour
{
    WebSocket ws;
    public GameObject Textchat;
    public GameObject ContainerContent;
    public TextMeshProUGUI TextName;
    public string _name;
    private string Chat;
    public Queue<Action> action;
    public TextMeshProUGUI InputName;
    public Button SetNameButton;

    public Player playername;

    char[] delimiterChars = { ':' };

    public void ClickToRespond(TMP_InputField input)
    {
        if (ws.IsAlive)
        {
            Debug.Log(ws.IsAlive);
            if (input.text != string.Empty)
            {
                Debug.Log(input.text);
                ws.Send(_name + ":" + input.text);
                input.text = string.Empty;
            }
        }
    }

    public void SetName()
    {
        if (InputName.text != string.Empty)
        {
            // playername.Name = _name;

            _name = InputName.text;
            TextName.text = _name;
            InputName.text = string.Empty;
        }
    }

    private void OnDestroy()
    {
        ws.Close();
        Debug.Log("Stop");
    }

    private void InstantGameObject(string chatMessage)
    {
        GameObject goo = Instantiate(Textchat, ContainerContent.transform);

        goo.GetComponent<TextMeshProUGUI>().text = chatMessage;
    }


    private void Update()
    {
        if (action?.Count > 0)
        {
            action.Dequeue()?.Invoke();
        }


    }

    // public event Action OnSetNameButtonClickHandler;

    private void OnSetNameButtonClick()
    {
        // OnSetNameButtonClickHandler?.Invoke();
        SetName();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNameButton.onClick.AddListener(OnSetNameButtonClick);
        action = new Queue<Action>();
        _name = "Annonymous";
        UnityThread.initUnityThread();
        ws = new WebSocket("ws://localhost:4610/Laputa");
        // Set the WebSocket events.

        ws.OnOpen += (sender, e) =>
        {

            Debug.Log("Case Open");
            // ws.Send("Ping");
        };

        ws.OnMessage += (sender, e) =>
        {
            // UnityThread.executeInUpdate(() =>
            // {
            //     GameObject goo = Instantiate(Textchat, ContainerContent.transform);
            //     goo.GetComponent<TextMeshProUGUI>().text = e.Data;
            // });
            Chat = e.Data;
            string[] words = Chat.Split(delimiterChars);
            string chatMessage = $"{words[0]}> {words[1]}";

            action.Enqueue(() => InstantGameObject(chatMessage));
            // message = e.Data;
            // Thread.Sleep(1000);
            // GameObject goo = Instantiate(Textchat, ContainerContent.transform);
            // goo.GetComponent<TextMeshProUGUI>().text = e.Data;

            // goo.GetComponent<TextMeshProUGUI>().text = e.Data;
        };

        ws.OnError += (sender, e) =>
        {
            var fmt = "[WebSocket Error] {0}";

            Debug.Log(fmt + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            var fmt = "[WebSocket Close ({0})] {1}";

            Debug.Log(fmt + e.Code + e.Reason);
        };
#if DEBUG
        // To change the logging level.
        ws.Log.Level = LogLevel.Trace;

        // To change the wait time for the response to the Ping or Close.
        //ws.WaitTime = TimeSpan.FromSeconds (10);

        // To emit a WebSocket.OnMessage event when receives a ping.
        //ws.EmitOnPing = true;
#endif
        // To enable the Per-message Compression extension.
        //ws.Compression = CompressionMethod.Deflate;

        // To validate the server certificate.
        /*
        ws.SslConfiguration.ServerCertificateValidationCallback =
          (sender, certificate, chain, sslPolicyErrors) => {
            var fmt = "Certificate:\n- Issuer: {0}\n- Subject: {1}";
            var msg = String.Format (
                        fmt, certificate.Issuer, certificate.Subject
                      );
            ws.Log.Debug (msg);
            return true; // If the server certificate is valid.
          };
         */

        // To send the credentials for the HTTP Authentication (Basic/Digest).
        //ws.SetCredentials ("nobita", "password", false);

        // To send the Origin header.
        //ws.Origin = "http://localhost:4649";

        // To send the cookies.
        //ws.SetCookie (new Cookie ("name", "nobita"));
        //ws.SetCookie (new Cookie ("roles", "\"idiot, gunfighter\""));

        // To connect through the HTTP Proxy server.
        //ws.SetProxy ("http://localhost:3128", "nobita", "password");

        // To enable the redirection.
        //ws.EnableRedirection = true;

        // Connect to the server.
        ws.Connect();

        // Connect to the server asynchronously.
        // ws.ConnectAsync();

        Debug.Log(ws.IsAlive);

        // ws.Send("YO");
        // Console.WriteLine("\nType 'exit' to exit.\n");

        // while (true)
        // {
        //     Thread.Sleep(1000);
        //     Console.Write("> ");

        //     var msg = Console.ReadLine();

        //     if (msg == "exit")
        //         break;

        //     // Send a text message.
        //     ws.Send(msg);
        // }
    }
}


