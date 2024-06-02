public interface IRobotState
{
    void EnterState(RobotController robot);
    void UpdateState(RobotController robot);
    void HandlerRightClick(RobotController robot);
    void HandlerLeftClick(RobotController robot);

}
