using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace Sanicball
{
    public class MatchStarter : MonoBehaviour
    {
        public MatchManager prefabToUse;

        public void StartSingleplayer()
        {
            var manager = Instantiate(prefabToUse);
        }

        public void StartServer()
        {
            var match = gameObject.AddComponent<NetworkMatch>();
            match.CreateMatch("Test test", 12, true, "", StartServerMatchCreated);
        }

        private void StartServerMatchCreated(CreateMatchResponse response)
        {
            if (response.success)
            {
                NetworkServer.Listen(61508);
                var client = ClientScene.ConnectLocalServer();
                ClientScene.RegisterPrefab(prefabToUse.gameObject);
                NetworkServer.Spawn(prefabToUse.gameObject);
            }
        }
    }
}