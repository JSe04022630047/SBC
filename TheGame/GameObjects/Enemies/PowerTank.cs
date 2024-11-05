namespace TheGame
{
    public class PowerTank : BaseEnemyTank
    {
        public PowerTank(int initX, int initY) : base(initX, initY, 3, 1 )
        {
            #region image
            ImgUp = Properties.Resources.powertankUp;
            ImgDown = Properties.Resources.powertankDown;
            ImgLeft = Properties.Resources.powertankLeft;
            ImgRight = Properties.Resources.powertankRight;
            makeTransparent();
            #endregion
        }
    }
}
