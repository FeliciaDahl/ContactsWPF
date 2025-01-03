namespace Dialog.ConsoleApp
{
    public interface IMenuDialog
    {
        void AddContact();
        string GetValidatedInput(string prompt, string properyName);
        void OutputDialog(string message);
        void QuitOption();
        void Run();
        void ShowContactList();
    }
}