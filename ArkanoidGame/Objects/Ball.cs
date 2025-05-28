using System;
using System.Drawing;

namespace ArkanoidGame.Objects
{
    public class Ball
    {
        // ѕозици€ м€ча
        public int X { get; private set; }
        public int Y { get; private set; }

        // Ќаправление движени€
        private float dx = 4;
        private float dy = -4;

        // ћножитель скорости и счетчик дл€ увеличени€ скорости
        private float speedMultiplier = 1.0f;
        private int updateCount = 0;
        private const int updatesUntilSpeedIncrease = 600;

        // –азмеры м€ча и его изображение
        public int Width { get; }
        public int Height { get; }
        private Image image;

        //  онструктор м€ча
        public Ball(int x, int y, Image image, int width = 20, int height = 20)
        {
            X = x;
            Y = y;
            this.image = image;
            Width = width;
            Height = height;
        }

        // ƒвижение м€ча с постепенным ускорением
        public void Move()
        {
            updateCount++;
            if (updateCount >= updatesUntilSpeedIncrease)
                speedMultiplier += 0.1f;
                updateCount = 0;

            X += (int)(dx * speedMultiplier);
            Y += (int)(dy * speedMultiplier);
        }

        // »зменение направлени€ движени€
        public void ReverseX() => dx = -dx;
        public void ReverseY() => dy = -dy;

        // ќтскок от платформы с учетом точки удара
        public void BounceFromPaddle(int paddleCenter)
        {
            var ballCenter = X + Width / 2;
            var distanceFromCenter = ballCenter - paddleCenter;
            var angle = distanceFromCenter * 0.7f;
            var speed = (float)Math.Sqrt(dx * dx + dy * dy);
            SetDirectionFromAngle(angle, speed);
        }

        // ”становка направлени€ под заданным углом
        public void SetDirectionFromAngle(float angleDeg, float speed)
        {
            var angleRad = angleDeg * (float)Math.PI / 180f;
            dx = speed * (float)Math.Sin(angleRad);
            dy = -speed * (float)Math.Cos(angleRad);
        }

        // —брос состо€ни€ м€ча
        public void Reset(int x, int y)
        {
            X = x;
            Y = y;
            dx = 4;
            dy = -4;
            speedMultiplier = 1.0f;
            updateCount = 0;
        }

        // ќтрисовка м€ча
        public void Draw(Graphics g) => g.DrawImage(image, X, Y, Width, Height);

        // √раницы м€ча дл€ коллизий
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
    }
}