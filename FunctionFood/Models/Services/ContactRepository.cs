using FunctionFood.Data;
using FunctionFood.Models.Interfaces;

namespace FunctionFood.Models.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly FunctionalFoodDatabaseContext _context;
        public ContactRepository(FunctionalFoodDatabaseContext context)
        {
            _context = context;
        }
        public void Add(ContactMessage message) => _context.ContactMessages.Add(message);

        public List<ContactMessage> GetLatest(int count = 5)
        {
            return _context.ContactMessages
                .OrderByDescending(m => m.SentAt)
                .Take(count)
                .ToList();
        }

        public int CountUnread() => _context.ContactMessages.Count(m => !m.IsRead);

        public void SaveChanges() => _context.SaveChanges();
    }
}
