using System;
using System.Linq;
using System.Collections.Generic;
using Unisave.Facades;
using Unisave.Facets;
using Unisave.Utils;

namespace Backend
{
    public class PlayerDataFacet : Facet
    {
        public PlayerEntity GetAuthorizedPlayer() => Auth.GetPlayer<PlayerEntity>();

        public List<TeamEntity> GetPlayerTeams() => GetAuthorizedPlayer().Teams.OrderBy(team => team.Title).ToList();

        public Tuple<string, int> GetPlayerData(string email)
        {
            var player = Auth.GetPlayer<PlayerEntity>();
            return Tuple.Create(player.Nickname, player.TotalScore);
        }

        public List<Tuple<string, int>> GetGeneralLeaderboard()
        {
            var players = DB.TakeAll<PlayerEntity>().Get();
            return GetLeaderboardData(players);
        }

        public List<Tuple<string, int>> GetTeamLeaderboardByOrderNumber(int orderNumber)
        {
            var team = GetPlayerTeams()[orderNumber];
            return GetLeaderboardData(team.Players);
        }

        public List<Tuple<string, int>> GetLeaderboardData(List<PlayerEntity> players)
        {
            return players
                .OrderByDescending(entity => entity.TotalScore)
                .ThenBy(entity => entity.Nickname)
                .Select(entity => Tuple.Create(entity.Nickname, entity.TotalScore))
                .ToList();
        }

        public string SendPlayerData(int levelNumber, int newScore)
        {
            var player = Auth.GetPlayer<PlayerEntity>();
            if (newScore > player.ScoresPerLevel[levelNumber])
            {
                player.ScoresPerLevel[levelNumber] = newScore;
                player.TotalScore += newScore;
                player.Save();
            }
            return string.Format("На счёт {0} начислено {1} очков!", player.Nickname, newScore);
        }
    }
}
