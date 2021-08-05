using System;
using Unisave.Examples.PlayerAuthentication.Backend;
using Unisave.Facades;
using Unisave.Facets;
using Unisave.Utils;

public class GetPlayerDataFacet : Facet
{
    public Tuple<string, int> GetPlayerData(string email)
    {
        var player = Auth.GetPlayer<PlayerEntity>();
        return Tuple.Create(player.nickname, player.totalScore);
    }
}
