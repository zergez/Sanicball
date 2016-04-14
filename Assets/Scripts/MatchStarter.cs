using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace Sanicball
{
    public class MatchStarter : MonoBehaviour
    {
        public MatchManager prefabToUse;
        private NetworkClient client;

        public void StartSingleplayer()
        {
            var manager = Instantiate(prefabToUse);
        }

        public void StartServer()
        {
            LogFilter.currentLogLevel = LogFilter.Debug;
            var match = gameObject.AddComponent<NetworkMatch>();
            match.CreateMatch("Test test", 12, true, "", StartServerMatchCreated);
        }

        private void StartServerMatchCreated(CreateMatchResponse response)
        {
            if (response.success)
            {
                NetworkServer.Listen(61508);
                client = ClientScene.ConnectLocalServer();
                client.RegisterHandler(MsgType.Connect, OnLocalClientConnect);
            }
        }

        private void OnLocalClientConnect(NetworkMessage netMsg)
        {
            ClientScene.Ready(netMsg.conn);
            Debug.Log("Bingus");
            ClientScene.RegisterPrefab(prefabToUse.gameObject);
            Debug.Log("Dingus");
            var ins = Instantiate(prefabToUse.gameObject);
            NetworkServer.Spawn(ins);
        }
    }
}