﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanicball;

namespace SanicballServerLib
{
    public class ServPlayer
    {
        public Guid ClientGuid { get; }
        public ControlType CtrlType { get; }
        public int CharacterId { get; set; }
        public bool ReadyToRace { get; set; }

        public bool CurrentlyRacing { get; set; }
        public Stopwatch RacingTimeout { get; }
        public bool TimeoutMessageSent { get; set; }

        public ServPlayer(Guid clientGuid, ControlType ctrlType, int initialCharacterId)
        {
            RacingTimeout = new Stopwatch();
            ClientGuid = clientGuid;
            CtrlType = ctrlType;
            CharacterId = initialCharacterId;
        }
    }
}