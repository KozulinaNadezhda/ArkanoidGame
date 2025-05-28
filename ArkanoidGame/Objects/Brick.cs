using System;
using System.Drawing;

namespace ArkanoidGame.Objects
{
    public class Brick
    {
        // Координаты и размеры кирпича
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        // Цвет и характеристики кирпича
        private Color color;
        private bool isMultiHit;
        private int hitPoints;
        public bool IsMultiHit => isMultiHit;

        // Конструктор кирпича
        public Brick(int x, int y, int width, int height, Color color, bool isMultiHit)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            this.color = color;
            this.isMultiHit = isMultiHit;
            hitPoints = isMultiHit ? 3 : 1;
        }

        // Обработка попадания по кирпичу
        public bool RegisterHit()
        {
            hitPoints--;

            if (isMultiHit)
            {
                switch (hitPoints)
                {
                    case 2:
                        color = Color.Orange;
                        break;
                    case 1:
                        color = Color.Yellow;
                        break;
                }
            }

            return hitPoints <= 0;
        }

        // Отрисовка кирпича
        public void Draw(Graphics g)
        {
            using var brush = new SolidBrush(color);
            g.FillRectangle(brush, X, Y, Width, Height);

            using var pen = new Pen(Color.Black);
            g.DrawRectangle(pen, X, Y, Width, Height);
        }

        // Границы кирпича для коллизий
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
    }
}
