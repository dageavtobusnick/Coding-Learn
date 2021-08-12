using System;
using System.Collections.Generic;
using Unisave.Entities;

namespace Backend
{
    public class PlayerEntity : Entity
    {
        public string Email;
        public string Password;
        public DateTime LastLoginAt = DateTime.UtcNow;

        public string Nickname;
        public int TotalScore;
        public List<int> ScoresPerLevel;

        public List<TeamEntity> Teams;
    }
}
