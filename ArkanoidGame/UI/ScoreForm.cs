using System;
using System.Drawing;
using System.Windows.Forms;
using ArkanoidGame.Game;

namespace ArkanoidGame.UI
{
    public class ScoreForm : Form
    {
        // Форма для отображения таблицы рекордов
        public ScoreForm()
        {
            // Настройка формы
            Text = "Результаты игр";
            Size = new Size(400, 400);
            BackColor = Color.Black;

            // Создание списка для отображения результатов
            var listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 12),
                BackColor = Color.Black,
                ForeColor = Color.LightGreen,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 24
            };

            // Загрузка результатов из менеджера
            var scores = ScoreManager.LoadScores();

            // Добавление результатов или сообщения об их отсутствии
            if (scores.Count == 0)
                listBox.Items.Add("Нет сохранённых результатов.");
            else
                foreach (var score in scores)
                    listBox.Items.Add(score);

            // Отрисовка элементов списка
            listBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                if (e.Index < 0)
                    return;

                var text = listBox.Items[e.Index].ToString();
                var brush = e.Index == 0 ? Brushes.Red : Brushes.LightGreen;

                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                e.DrawFocusRectangle();
            };

            // Добавление списка на форму
            Controls.Add(listBox);
        }
    }
}

