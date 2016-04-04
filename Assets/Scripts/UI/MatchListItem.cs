using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Sanicball.UI
{
    public class MatchListItem : MonoBehaviour
    {
        public Text serverNameText;
        public Text serverStatusText;
        public Text playerCountText;
        public Text pingText;
        public Image pingLoadingImage;

        //TODO: Implement pinging matches
        //private bool pingDone = false;
        //private float pingTimeout = 8f;

        public void SetData(MatchDesc data)
        {
            //info = new ServerInfo(data);
            serverNameText.text = data.name;
            serverStatusText.text = "(STATUS GOES HERE)";
            playerCountText.text = data.currentSize + "/" + data.maxSize;
        }
    }
}