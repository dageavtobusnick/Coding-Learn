using System;
using System.Collections;
using System.Collections.Generic;
using Unisave;
using Unisave.Facets;
using Unisave.Facades;
using Unisave.Authentication.Middleware;
using System.Linq;

namespace Backend
{
    public enum CreatingTeamResponse
    {
        OK,
        IdExists,
        InvalidID,
        NullID,
        NullTitle,
        NullDescription
    }

    public enum JoinTeamResponse
    {
        OK,
        NullID,
        NullInviteCode,
        InvalidTeamID,
        InvalidInviteCode,
        PlayerExists
    }

    public class TeamConnectionFacet : Facet
    {
        public CreatingTeamResponse CreateTeam(string teamID, string teamTitle, string teamDescription)
        {
            if (teamID == null || teamID == "")
                return CreatingTeamResponse.NullID;
            if (teamTitle == null || teamTitle == "")
                return CreatingTeamResponse.NullTitle;
            if (teamDescription == null || teamDescription == "")
                return CreatingTeamResponse.NullDescription;
            if (teamID[0] != '@')
                return CreatingTeamResponse.InvalidID;
            var sameIdTeam = DB.TakeAll<TeamEntity>().Filter(team => team.ID == teamID).First();
            if (sameIdTeam == null)
            {
                var player = Auth.GetPlayer<PlayerEntity>();
                var newTeam = new TeamEntity()
                {
                    ID = teamID,
                    Title = teamTitle,
                    Description = teamDescription,
                    InviteCode = GenerateInviteCode(),
                    Owner = player,
                    Players = new List<PlayerEntity>()
                };
                newTeam.Players.Add(player);
                newTeam.Save();
                player.Teams.Add(newTeam);
                player.Save();
                return CreatingTeamResponse.OK;
            }
            return CreatingTeamResponse.IdExists;
        }

        public JoinTeamResponse JoinTeam(string teamID, string teamInviteCode)
        {
            if (teamID == null || teamID == "")
                return JoinTeamResponse.NullID;
            if (teamInviteCode == null || teamInviteCode == "")
                return JoinTeamResponse.NullInviteCode;
            var teamToJoin = DB.TakeAll<TeamEntity>().Filter(team => team.ID == teamID).First();
            if (teamToJoin == null)
                return JoinTeamResponse.InvalidTeamID;
            if (teamToJoin.InviteCode != teamInviteCode)
                return JoinTeamResponse.InvalidInviteCode;
            var currentPlayer = Auth.GetPlayer<PlayerEntity>();
            if (teamToJoin.Players.Contains(currentPlayer))
                return JoinTeamResponse.PlayerExists;
            teamToJoin.Players.Add(currentPlayer);
            teamToJoin.Save();
            currentPlayer.Teams.Add(teamToJoin);
            currentPlayer.Save();           
            return JoinTeamResponse.OK;
        }

        private string GenerateInviteCode()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 24).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
