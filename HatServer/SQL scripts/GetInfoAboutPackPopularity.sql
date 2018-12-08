select count_PackID, PackId, Name from ( select count(distr_packid.PackId) as count_PackID, distr_packid.PackId from
              (select distinct PackId, GameId from InGamePhrase) as distr_packid
              group by  distr_packid.PackId) as A
LEFT JOIN GamePacks  on  GamePacks.Id= A.PackId order by count_PackID desc