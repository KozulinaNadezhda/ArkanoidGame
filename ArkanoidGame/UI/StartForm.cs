using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArkanoidGame.UI
{
    public class StartForm : Form
    {
        // Кнопки меню
        private Button startButton;
        private Button scoreButton;

        public StartForm()
        {
            // Настройка формы главного меню
            Text = "Арканоид — Меню";
            Size = new Size(500, 320);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.Black;

            // Лейбл с инструкцией
            var instruction = new Label
            {
                Text = "Управление:\n" +
                       "- Двигайте платформу мышкой\n" +
                       "- Разбейте все блоки\n" +
                       "- Серые блоки разбиваются после 3 ударов\n" +
                       "- Цвета этих блоков показывают, сколько осталось ударов\n" +
                       "- У вас 3 жизни\n\nУдачи!",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 160
            };

            // Кнопка начала игры
            startButton = new Button
            {
                Text = "Начать игру",
                Width = 120,
                Height = 40,
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            startButton.FlatAppearance.BorderSize = 0;
            startButton.Click += (s, e) =>
            {
                Hide();
                var gameForm = new GameForm();
                gameForm.FormClosed += (sender, args) => Close();
                gameForm.Show();
            };

            // Кнопка просмотра рекордов
            scoreButton = new Button
            {
                Text = "Рекорды",
                Width = 120,
                Height = 35,
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            scoreButton.FlatAppearance.BorderSize = 0;
            scoreButton.Click += (s, e) => new ScoreForm().ShowDialog();

            // Позиционирование кнопок
            startButton.Location = new Point((ClientSize.Width - startButton.Width) / 2, 160);
            scoreButton.Location = new Point((ClientSize.Width - scoreButton.Width) / 2, 210);

            // Обработчик изменения размера формы
            Load += (s, e) =>
            {
                startButton.Left = (ClientSize.Width - startButton.Width) / 2;
                scoreButton.Left = (ClientSize.Width - scoreButton.Width) / 2;
            };

            // Добавление элементов на форму
            Controls.Add(instruction);
            Controls.Add(startButton);
            Controls.Add(scoreButton);
        }
    }
}
