SELECT Games.InGameId
    , Games.StartDate
    , getcountphrase.count_phrase
    , getcountrounds.count_round
    , B.time_game
    , (B.time_game/ getcountrounds.count_round) as time_round_average
FROM Games
       JOIN
         (select count(Id) as count_round
              , Rounds.GameId
          FROM Rounds GROUP BY GameId) as getcountrounds
         ON  getcountrounds.GameID = Games.InGameId
    
       JOIN
         (select InGamePhrase.GameId
              , count(InGamePhrase.Id) AS count_phrase 
          FROM InGamePhrase 
          GROUP BY InGamePhrase.GameId) AS getcountphrase
         ON Games.Id = getcountphrase.GameId
    
       JOIN (select Rounds.GameId
                 , Sum(A.Time_Round) as time_game 
             FROM Rounds 
                    JOIN 
                      (select RoundPhrases.RoundId, SUM(RoundPhrases.Time) as Time_Round 
                       from RoundPhrases GROUP BY RoundPhrases.RoundId)  as A
                                                                                                                                                                                                                                                         ON A.RoundId = Rounds.Id  GROUP BY GameId) as B   ON  B.GameId = Games.InGameId
ORDER BY StartDate desc 