SELECT Games.Id, Games.StartDate, getcountphrase.count_phrase, getcountrounds.count_round FROM  Games    
JOIN
(select count(Id) as count_round , Rounds.GameId  FROM Rounds GROUP BY GameId) as getcountrounds
ON  getcountrounds.GameID = Games.InGameId
JOIN
(select InGamePhrase.GameId, count(InGamePhrase.Id) AS count_phrase FROM InGamePhrase GROUP BY InGamePhrase.GameId) AS getcountphrase
ON Games.Id = getcountphrase.GameId

WHERE count_phrase >10
