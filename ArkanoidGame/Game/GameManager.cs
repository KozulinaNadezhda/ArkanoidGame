using System;
using System.Collections.Generic;
using System.Drawing;
using ArkanoidGame.Objects;

namespace ArkanoidGame.Game
{
    public class GameManager
    {
        private List<Brick> bricks;
        public int Score { get; private set; }
        public List<Brick> Bricks => bricks;  // Публичное свойство для доступа к кирпичам

        public GameManager(int clientWidth)
        {
            bricks = new List<Brick>();

            // Параметры для генерации кирпичей
            var spacing = 2;
            var cols = 13;
            var brickWidth = (clientWidth - (cols - 1) * spacing) / cols;
            var brickHeight = 20;
            var rows = 8;
            var topOffset = 2 * brickHeight;

            var rand = new Random();

            // Генерация сетки кирпичей
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    // 30% chance создать прочный кирпич
                    var isMultiHit = rand.NextDouble() < 0.3;
                    var color = isMultiHit ? Color.Gray : Color.Yellow;

                    var x = i * (brickWidth + spacing);
                    var y = topOffset + j * (brickHeight + spacing);

                    var brick = new Brick(x, y, brickWidth, brickHeight, color, isMultiHit);
                    bricks.Add(brick);
                }
            }
        }

        // Проверка столкновения мяча с кирпичами
        public void CheckBrickCollision(Ball ball, Paddle paddle)
        {
            // Используем ToArray() чтобы избежать ошибки при изменении коллекции во время итерации
            foreach (var brick in bricks.ToArray())
            {
                if (ball.Bounds.IntersectsWith(brick.Bounds))
                {
                    var shouldRemove = brick.RegisterHit();

                    if (shouldRemove)
                        bricks.Remove(brick);
                        Score += brick.IsMultiHit ? 3 : 1;  // Больше очков за прочные кирпичи

                    ball.ReverseY();  // Меняем направление мяча
                    break;  // Прерываем после первого столкновения
                }
            }
        }

        // Отрисовка всех кирпичей
        public void DrawBricks(Graphics g)
        {
            foreach (var brick in bricks)
                brick.Draw(g);
        }

        // Проверка, все ли кирпичи разрушены
        public bool AllBricksCleared => bricks.Count == 0;
    }
}
