SELECT word, COUNT(word) as count
FROM InGamePhrase
WHERE PackId < 1
GROUP BY word
ORDER BY count DESC 