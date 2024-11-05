namespace TheGame
{
    public class BasicTank : BaseEnemyTank
    {
        public BasicTank(int initX, int initY) : base(initX, initY, 3, 1 )
        {
            #region image
            ImgUp = Properties.Resources.basictankUp;
            ImgDown = Properties.Resources.basictankDown;
            ImgLeft = Properties.Resources.basictankLeft;
            ImgRight = Properties.Resources.basictankRight;
            #endregion
        }
    }
}
