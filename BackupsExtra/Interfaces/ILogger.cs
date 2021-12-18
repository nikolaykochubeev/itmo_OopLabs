namespace BackupsExtra.Interfaces
{
    public interface ILogger
    {
        void Log(string text, bool timeCodePrefix = false);
    }
}