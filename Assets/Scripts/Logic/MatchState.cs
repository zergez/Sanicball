﻿using System.Collections;
using System.Collections.Generic;
using Sanicball.Data;
using UnityEngine;

namespace Sanicball.Logic
{
    public class MatchState
    {
        public List<MatchClientState> Clients { get; private set; }
        public List<MatchPlayerState> Players { get; private set; }
        public MatchSettings Settings { get; private set; }
        public bool InRace { get; private set; }
        public float CurAutoStartTime { get; private set; }

        public MatchState(List<MatchClientState> clients, List<MatchPlayerState> players, MatchSettings settings, bool inRace, float curAutoStartTime)
        {
            Clients = clients;
            Players = players;
            Settings = settings;
            InRace = inRace;
            CurAutoStartTime = curAutoStartTime;
        }
    }

    public class MatchClientState
    {
        public System.Guid Guid { get; private set; }
        public string Name { get; private set; }

        public MatchClientState(System.Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }
    }

    public class MatchPlayerState
    {
        public System.Guid ClientGuid { get; private set; }
        public ControlType CtrlType { get; private set; }
        public bool ReadyToRace { get; private set; }
        public int CharacterId { get; private set; }

        public MatchPlayerState(System.Guid clientGuid, ControlType ctrlType, bool readyToRace, int characterId)
        {
            ClientGuid = clientGuid;
            CtrlType = ctrlType;
            ReadyToRace = readyToRace;
            CharacterId = characterId;
        }
    }
}
