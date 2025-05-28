using System.Drawing;

namespace ArkanoidGame.Objects
{
    public class Paddle
    {
        // ������� � ������� ���������
        public int X { get; private set; }
        public int Y { get; }
        public int Width { get; private set; }
        public int Height { get; }

        // ������������ ������ (��� ���������� ������)
        private readonly int originalWidth;

        // ����������� ���������
        private Image image;

        // ����� ��������� �� X
        public int CenterX => X + Width / 2;

        // ����������� ���������
        public Paddle(int x, int y, Image image, int width = 100, int height = 20)
        {
            X = x;
            Y = y;
            this.image = image;
            Width = originalWidth = width;
            Height = height;
        }

        // ����������� ��������� � ��������� ������� ����
        public void MoveTo(int mouseX, int clientWidth)
        {
            X = mouseX - Width / 2;
            if (X < 0) X = 0;
            if (X + Width > clientWidth) X = clientWidth - Width;
        }

        // ��������� ���������
        public void Draw(Graphics g) => g.DrawImage(image, X, Y, Width, Height);

        // ������� ��������� ��� ��������
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
    }
}