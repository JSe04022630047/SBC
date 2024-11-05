namespace TheGame
{
    public class ArmorTank : BaseEnemyTank
    {
        public ArmorTank(int initX, int initY) : base(initX, initY, 2, 3, 8, 3)
        {
            #region image
            ImgUp = Properties.Resources.armortankUp;
            ImgDown = Properties.Resources.armortankDown;
            ImgLeft = Properties.Resources.armortankLeft;
            ImgRight = Properties.Resources.armortankRight;
            makeTransparent();
            #endregion
        }
    }
}
