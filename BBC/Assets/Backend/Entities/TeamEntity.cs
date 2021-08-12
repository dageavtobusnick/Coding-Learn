using System;
using System.Collections;
using System.Collections.Generic;
using Unisave;
using Unisave.Entities;
using Unisave.Facades;

namespace Backend
{
    public class TeamEntity : Entity
    {
        public string ID;
        public string InviteCode;
        public string Title;
        public string Description;
        public PlayerEntity Owner;
        public List<PlayerEntity> Players;
    }
}
