using EnumTypes;

public interface IGame : IManager
{
    public void Notify(NotifyType notifyType, string message);
}