namespace Business.Interfaces
{
    public interface IFileService
    {
        string LoadListFromFile();
        bool SaveListToFile(string content);
    }
}