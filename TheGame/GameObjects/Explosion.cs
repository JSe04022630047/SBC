using System.Drawing;

namespace TheGame
{
    public class Explosion : NoMovingObj
    {
        public bool IsDestory { get; set; }
        private int playSpeed = 2;
        private int playCount = 0;
        private int playFrame = 0;

        private Bitmap[] animation = new Bitmap[]
        {
            Properties.Resources.explosion0,
            Properties.Resources.explosion1,
            Properties.Resources.explosion2
        };

        public Explosion(int x, int y) : base(x,y, new Bitmap(16,16)) { IsDestory = false; }

        public override void Update()
        {
            playCount++;
            playFrame = (playCount - 1) / playSpeed;
            if (playFrame > 2)
            {
                IsDestory = true;
            }
            base.Update();
        }

        protected override Image GetImg()
        {
            Bitmap bmp;
            if (playFrame >= 2) bmp = animation[2];
            else bmp = animation[playFrame];
            bmp.MakeTransparent(Color.FromArgb(24, 255, 0));
            return bmp;
        }

        public override void DrawS()
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImg(), X - Width / 2, Y - Height / 2);
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle(X - Width / 2, Y - Height / 2, Width, Height);
        }
    }
}
