using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArkanoidGame
{
    public class GameOverForm : Form
    {
        // Форма окончания игры
        public GameOverForm(int score, int elapsedSeconds)
        {
            // Настройка формы
            Text = "Игра окончена";
            Size = new Size(300, 200);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Метка с результатами игры
            var resultLabel = new Label
            {
                Text = $"Счёт: {score}\nВремя: {elapsedSeconds / 60:D2}:{elapsedSeconds % 60:D2}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };

            // Кнопка перезапуска игры
            var restartButton = new Button
            {
                Text = "Начать заново",
                Dock = DockStyle.Top,
                Height = 40
            };
            restartButton.Click += (s, e) =>
            {
                Hide();
                Application.Restart();
            };

            // Кнопка выхода из игры
            var exitButton = new Button
            {
                Text = "Выйти",
                Dock = DockStyle.Top,
                Height = 40
            };
            exitButton.Click += (s, e) => Application.Exit();

            // Добавление элементов на форму
            Controls.Add(exitButton);
            Controls.Add(restartButton);
            Controls.Add(resultLabel);
        }
    }
}