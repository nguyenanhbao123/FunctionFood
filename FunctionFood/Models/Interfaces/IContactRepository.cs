namespace FunctionFood.Models.Interfaces
{
    public interface IContactRepository
    {
        void Add(ContactMessage message);
        List<ContactMessage> GetLatest(int count = 5);
        int CountUnread();
        void SaveChanges();
    }
}
