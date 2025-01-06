namespace Business.Interfaces
{
    public interface IFileService
    {

        bool SaveListToFile(string content);
        string LoadListFromFile();
     
    }
}