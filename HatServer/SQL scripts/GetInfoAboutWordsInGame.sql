SELECT State, Time, Word, BadItalic, PackId, GameId
FROM (SELECT RP.State, RP.WordId, RP.Time, RP.RoundId, SelectedGames.stringId, SelectedGames.intId
      FROM RoundPhrases RP
             JOIN Rounds R on RP.RoundId = R.Id
             JOIN (SELECT G.Id as intId, G.InGameId as stringId, G.StartDate FROM Games G) as SelectedGames
               on SelectedGames.stringId = R.GameId) as UnionTable
       JOIN InGamePhrase IGP ON IGP.GameId = UnionTable.intId AND IGP.InGameId = UnionTable.WordId
WHERE UnionTable.RoundId > 6101
GO