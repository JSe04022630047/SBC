namespace TheGame
{
    public class FastTank : BaseEnemyTank
    {
        public FastTank(int initX, int initY) : base(initX, initY, 6, 1, 5, 2)
        {
            #region image
            ImgUp = Properties.Resources.fastTankUp;
            ImgDown = Properties.Resources.fastTankDown;
            ImgLeft = Properties.Resources.fastTankLeft;
            ImgRight = Properties.Resources.fastTankRight;
            makeTransparent();
            #endregion
        }
    }
}
