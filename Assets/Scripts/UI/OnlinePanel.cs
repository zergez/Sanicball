using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Sanicball.UI
{
    public class OnlinePanel : MonoBehaviour
    {
        public Transform targetServerListContainer;
        public Image spinnerImage;
        public Text errorField;
        public Text serverCountField;
        public MatchListItem matchButtonPrefab;
        public Selectable aboveList;
        public Selectable belowList;
        private string masterServerGameName = "sanicball";
        private bool refreshing = false;

        private NetworkMatch matchFinder;
        private List<MatchListItem> latestMatches = new List<MatchListItem>();

        private void Awake()
        {
            matchFinder = gameObject.AddComponent<NetworkMatch>();
            masterServerGameName = "sanicball" + GameVersion.AsFloat;
            errorField.enabled = false;
        }

        private void Update()
        {
            //Refresh on f5 (pretty nifty)
            if (Input.GetKeyDown(KeyCode.F5))
            {
                RefreshMatches();
            }
        }

        public void RefreshMatches()
        {
            if (refreshing) return;

            Debug.Log("Listing matches...");

            serverCountField.text = "Refreshing...";
            spinnerImage.enabled = true;
            refreshing = true;
            errorField.enabled = false;
            //Clear old matches
            foreach (var serv in latestMatches)
            {
                Destroy(serv.gameObject);
            }
            latestMatches.Clear();

            matchFinder.ListMatches(0, 10, "", ListMatchesCallback);
        }

        private void ListMatchesCallback(ListMatchResponse response)
        {
            if (!response.success)
            {
                errorField.enabled = true;
                errorField.text = "Could not connect to the matchmaking service\n(" + response.extendedInfo + ")";
                return;
            }

            Debug.Log(response.matches.Count + " matches found");
            refreshing = false;
            spinnerImage.enabled = false;

            serverCountField.text = response.matches.Count + " active matches";
            if (response.matches.Count > 0)
            {
                errorField.enabled = false;
            }
            else
            {
                errorField.enabled = true;
                errorField.text = @"No matches to join ¯\_(ツ)_/¯";
            }

            //Create match list items for all matches found
            foreach (var match in response.matches)
            {
                var matchListItem = Instantiate(matchButtonPrefab);
                matchListItem.transform.SetParent(targetServerListContainer, false);
                matchListItem.SetData(match);
                latestMatches.Add(matchListItem);
            }

            //Add navigation links
            for (var i = 0; i < latestMatches.Count; i++)
            {
                var button = latestMatches[i].GetComponent<Button>();
                if (button)
                {
                    var nav = new Navigation() { mode = Navigation.Mode.Explicit };
                    //Up navigation
                    if (i == 0)
                    {
                        nav.selectOnUp = aboveList;
                        var nav2 = aboveList.navigation;
                        nav2.selectOnDown = button;
                        aboveList.navigation = nav2;
                    }
                    else
                    {
                        nav.selectOnUp = latestMatches[i - 1].GetComponent<Button>();
                    }
                    //Down navigation
                    if (i == latestMatches.Count - 1)
                    {
                        nav.selectOnDown = belowList;
                        var nav2 = belowList.navigation;
                        nav2.selectOnUp = button;
                        belowList.navigation = nav2;
                    }
                    else
                    {
                        nav.selectOnDown = latestMatches[i + 1].GetComponent<Button>();
                    }

                    button.navigation = nav;
                }
            }
        }
    }
}