using System;
using System.Drawing;

namespace ArkanoidGame.Objects
{
    public class Ball
    {
        // ������� ����
        public int X { get; private set; }
        public int Y { get; private set; }

        // ����������� ��������
        private float dx = 4;
        private float dy = -4;

        // ��������� �������� � ������� ��� ���������� ��������
        private float speedMultiplier = 1.0f;
        private int updateCount = 0;
        private const int updatesUntilSpeedIncrease = 600;

        // ������� ���� � ��� �����������
        public int Width { get; }
        public int Height { get; }
        private Image image;

        // ����������� ����
        public Ball(int x, int y, Image image, int width = 20, int height = 20)
        {
            X = x;
            Y = y;
            this.image = image;
            Width = width;
            Height = height;
        }

        // �������� ���� � ����������� ����������
        public void Move()
        {
            updateCount++;
            if (updateCount >= updatesUntilSpeedIncrease)
                speedMultiplier += 0.1f;
                updateCount = 0;

            X += (int)(dx * speedMultiplier);
            Y += (int)(dy * speedMultiplier);
        }

        // ��������� ����������� ��������
        public void ReverseX() => dx = -dx;
        public void ReverseY() => dy = -dy;

        // ������ �� ��������� � ������ ����� �����
        public void BounceFromPaddle(int paddleCenter)
        {
            var ballCenter = X + Width / 2;
            var distanceFromCenter = ballCenter - paddleCenter;
            var angle = distanceFromCenter * 0.7f;
            var speed = (float)Math.Sqrt(dx * dx + dy * dy);
            SetDirectionFromAngle(angle, speed);
        }

        // ��������� ����������� ��� �������� �����
        public void SetDirectionFromAngle(float angleDeg, float speed)
        {
            var angleRad = angleDeg * (float)Math.PI / 180f;
            dx = speed * (float)Math.Sin(angleRad);
            dy = -speed * (float)Math.Cos(angleRad);
        }

        // ����� ��������� ����
        public void Reset(int x, int y)
        {
            X = x;
            Y = y;
            dx = 4;
            dy = -4;
            speedMultiplier = 1.0f;
            updateCount = 0;
        }

        // ��������� ����
        public void Draw(Graphics g) => g.DrawImage(image, X, Y, Width, Height);

        // ������� ���� ��� ��������
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
    }
}