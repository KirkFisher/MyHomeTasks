public abstract class GameMode
{
    public abstract void EnterMode(RobotController robot);
    public abstract void UpdateMode(RobotController robot);
    public abstract void HandlerRightClick(RobotController robot);
    public abstract void HandlerLeftClick(RobotController robot);
}
