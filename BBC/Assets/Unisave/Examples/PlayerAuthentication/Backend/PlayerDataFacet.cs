using System;
using System.Linq;
using System.Collections.Generic;
using Unisave.Examples.PlayerAuthentication.Backend;
using Unisave.Facades;
using Unisave.Facets;
using Unisave.Utils;

public class PlayerDataFacet : Facet
{
    public PlayerEntity IsPlayerAuthorized() => Auth.GetPlayer<PlayerEntity>();

    public Tuple<string, int> GetPlayerData(string email)
    {
        var player = Auth.GetPlayer<PlayerEntity>();
        return Tuple.Create(player.nickname, player.totalScore);
    }

    public List<Tuple<string, int>> GetLeaderboardData()
    {
        var leaderboardData = DB.TakeAll<PlayerEntity>()
            .Get()
            .OrderByDescending(entity => entity.totalScore)
            .ThenBy(entity => entity.nickname)
            .Select(entity => Tuple.Create(entity.nickname, entity.totalScore))
            .ToList();
        return leaderboardData;
    }

    public string SendPlayerData(int levelNumber, int newScore)
    {
        var player = Auth.GetPlayer<PlayerEntity>();
        if (newScore > player.scoresPerLevel[levelNumber])
        {
            player.scoresPerLevel[levelNumber] = newScore;
            player.totalScore += newScore;
            player.Save();
        }
        return string.Format("На счёт {0} начислено {1} очков!", player.nickname, newScore);
    }
}
