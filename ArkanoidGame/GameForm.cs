using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ArkanoidGame.Objects;
using ArkanoidGame.Game;
using ArkanoidGame.UI;

namespace ArkanoidGame
{
    public class GameForm : Form
    {
        // Основные игровые компоненты
        private Timer gameTimer;
        private Ball ball;
        private Paddle paddle;
        private GameManager gameManager;

        // Статистика игры
        private int lives = 3;
        private int elapsedSeconds = 0;

        // Графические ресурсы
        private Image heartImage;
        private Image paddleImage;
        private Image ballImage;

        public GameForm()
        {
            // Настройка формы
            DoubleBuffered = true;
            Width = 860;
            Height = 700;
            Text = "Арканоид";
            BackColor = Color.Black;

            LoadAssets();

            // Инициализация игровых объектов
            paddle = new Paddle(400, 580, paddleImage);
            ball = new Ball(paddle.X + 25, 560, ballImage);
            gameManager = new GameManager(ClientSize.Width);

            InitTimers();
            MouseMove += OnMouseMove;
        }

        // Загрузка графических ресурсов
        private void LoadAssets()
        {
            var assetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            paddleImage = Image.FromFile(Path.Combine(assetsPath, "paddle.png"));
            ballImage = Image.FromFile(Path.Combine(assetsPath, "ball.png"));
            heartImage = Image.FromFile(Path.Combine(assetsPath, "heart.png"));
        }

        // Инициализация таймеров
        private void InitTimers()
        {
            gameTimer = new Timer { Interval = 1000 / 60 };
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            var secondTimer = new Timer { Interval = 1000 };
            secondTimer.Tick += (s, e) => elapsedSeconds++;
            secondTimer.Start();
        }

        // Обработка движения мыши
        private void OnMouseMove(object sender, MouseEventArgs e) => paddle.MoveTo(e.X, ClientSize.Width);

        // Основной игровой цикл
        private void GameLoop(object sender, EventArgs e)
        {
            ball.Move();

            // Обработка столкновений со стенами
            if (ball.Bounds.Left <= 0 || ball.Bounds.Right >= ClientSize.Width)
                ball.ReverseX();

            if (ball.Bounds.Top <= 0)
                ball.ReverseY();

            // Обработка потери жизни
            if (ball.Bounds.Bottom >= ClientSize.Height)
            {
                lives--;
                if (lives <= 0)
                {
                    gameTimer.Stop();
                    ScoreManager.SaveScore(gameManager.Score, elapsedSeconds);
                    new GameOverForm(gameManager.Score, elapsedSeconds).ShowDialog();
                    Close();
                    return;
                }
                else
                    ball.Reset(paddle.X + 25, 560);
            }

            // Обработка столкновения с платформой
            if (ball.Bounds.IntersectsWith(paddle.Bounds))
                ball.BounceFromPaddle(paddle.CenterX);

            // Проверка столкновений с кирпичами
            gameManager.CheckBrickCollision(ball, paddle);

            // Проверка завершения уровня
            if (gameManager.AllBricksCleared)
            {
                gameTimer.Stop();
                ScoreManager.SaveScore(gameManager.Score, elapsedSeconds);
                new GameOverForm(gameManager.Score, elapsedSeconds).ShowDialog();
                Close();
                return;
            }

            Invalidate();
        }

        // Отрисовка игровых объектов
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            paddle.Draw(g);
            ball.Draw(g);
            gameManager.DrawBricks(g);

            // Отрисовка статистики
            using var font = new Font("Arial", 14, FontStyle.Bold);
            g.DrawString($"Счёт: {gameManager.Score}", font, Brushes.White, 10, 10);
            g.DrawString($"Время: {elapsedSeconds / 60:D2}:{elapsedSeconds % 60:D2}", font, Brushes.White, 150, 10);

            // Отрисовка жизней
            for (int i = 0; i < lives; i++)
                g.DrawImage(heartImage, ClientSize.Width - (i + 1) * 32 - 100, 10, 28, 28);
        }
    }
}
