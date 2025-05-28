using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArkanoidGame.Game
{
    public static class ScoreManager
    {
        // Путь к файлу с рекордами (в директории приложения)
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scores.txt");

        // Сохраняет результат игры (очки и время) в файл
        public static void SaveScore(int score, int timeInSeconds)
        {
            var line = $"{DateTime.Now}|{score}|{timeInSeconds}";
            File.AppendAllLines(filePath, new[] { line });
        }

        // Загружает и обрабатывает сохраненные результаты
        public static List<string> LoadScores()
        {
            // Список для хранения распарсенных результатов
            var results = new List<(DateTime Date, int Score, int Time, string Line)>();

            // Проверка существования файла перед чтением
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3 &&
                        DateTime.TryParse(parts[0], out var date) &&
                        int.TryParse(parts[1], out var score) &&
                        int.TryParse(parts[2], out var time))
                    {
                        results.Add((date, score, time, line));
                    }
                }
            }

            // Сортировка результатов: сначала по очкам (убывание), затем по времени (возрастание)
            var sorted = results
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.Time)
                .ToList();

            // Форматирование результатов для отображения
            var displayList = new List<string>();
            foreach (var r in sorted)
            {
                string formattedTime = $"{r.Time / 60:D2}:{r.Time % 60:D2}";
                displayList.Add($"{r.Date:dd.MM.yyyy HH:mm}: Очки - {r.Score}, Время - {formattedTime}");
            }

            return displayList;
        }
    }
}

