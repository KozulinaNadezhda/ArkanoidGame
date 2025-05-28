using System.Drawing;

namespace ArkanoidGame.Objects
{
    public class Paddle
    {
        // Позиция и размеры платформы
        public int X { get; private set; }
        public int Y { get; }
        public int Width { get; private set; }
        public int Height { get; }

        // Оригинальная ширина (для возможного сброса)
        private readonly int originalWidth;

        // Изображение платформы
        private Image image;

        // Центр платформы по X
        public int CenterX => X + Width / 2;

        // Конструктор платформы
        public Paddle(int x, int y, Image image, int width = 100, int height = 20)
        {
            X = x;
            Y = y;
            this.image = image;
            Width = originalWidth = width;
            Height = height;
        }

        // Перемещение платформы к указанной позиции мыши
        public void MoveTo(int mouseX, int clientWidth)
        {
            X = mouseX - Width / 2;
            if (X < 0) X = 0;
            if (X + Width > clientWidth) X = clientWidth - Width;
        }

        // Отрисовка платформы
        public void Draw(Graphics g) => g.DrawImage(image, X, Y, Width, Height);

        // Границы платформы для коллизий
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
    }
}