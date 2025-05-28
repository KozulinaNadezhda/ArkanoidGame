using System;
using System.Drawing;
using System.IO;

namespace ArkanoidGame.Game
{
    public class GameStats
    {
        public int Score { get; private set; }
        public int Lives { get; private set; }
        public TimeSpan GameTime { get; private set; }

        private DateTime startTime;
        private Image heartImage;  // Изображение для отображения жизней

        public GameStats()
        {
            Score = 0;
            Lives = 3;
            startTime = DateTime.Now;

            // Загрузка изображения сердца из папки Assets
            var heartPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "heart.png");
            heartImage = File.Exists(heartPath) ? Image.FromFile(heartPath) : null;
        }

        // Увеличение счета на 1
        public void IncreaseScore() => Score++;

        // Уменьшение количества жизней
        public void LoseLife() => Lives--;

        // Обновление игрового времени
        public void UpdateTime() => GameTime = DateTime.Now - startTime;

        // Отрисовка статистики игры
        public void Draw(Graphics g)
        {
            g.DrawString($"Очки: {Score}", new Font("Arial", 12), Brushes.White, 10, 5);

            // Форматирование времени в MM:SS
            var timeStr = $"Время: {GameTime.Minutes:D2}:{GameTime.Seconds:D2}";
            g.DrawString(timeStr, new Font("Arial", 12), Brushes.White, 120, 5);

            if (heartImage != null)
                // Отрисовка сердечек по количеству жизней
                for (int i = 0; i < Lives; i++)
                    g.DrawImage(heartImage, 250 + i * 30, 2, 24, 24);
            else
                // Если нет изображения, отображаем текстом
                g.DrawString($"Жизни: {Lives}", new Font("Arial", 12), Brushes.White, 250, 5);
        }

        // Проверка окончания игры (когда жизни закончились)
        public bool IsGameOver() => Lives <= 0;
    }
}
