using BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO? instance;
        private static readonly object lockObject = new object();
        private MemberDAO() { }
        public static MemberDAO Instance()
        {

            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new MemberDAO();
                }
                return instance;
            }
        }
 
        public async Task<List<Member>> GetMembers()
        {
            using (var context = new EStoreContext())
            {
                return await context.Members.ToListAsync();
            }
        }

        public async Task<Member?> GetMember(int memberID)
        {
            using (var context = new EStoreContext())
            {
                return await context.Members.FindAsync(memberID);
            }
        }

        public async Task<int> AddMember(Member member)
        {
            using (var context = new EStoreContext())
            {
                await context.Members.AddAsync(member);
                await context.SaveChangesAsync();
                return member.MemberId;
            }
        }

        public async Task<int> UpdateMember(int id, Member member)
        {
            using (var context = new EStoreContext())
            {
                var memberToUpdate = await context.Members.FindAsync(id);

                if (memberToUpdate is null)
                {
                    return -1;
                }
                memberToUpdate.City = member.City;
                memberToUpdate.Country = member.Country;
                memberToUpdate.Password = member.Password;
                memberToUpdate.CompanyName = member.CompanyName;
                memberToUpdate.Email = member.Email;
                await context.SaveChangesAsync();
                return memberToUpdate.MemberId;
            }
        }

        public async Task<int> DeleteMember(int memberID)
        {
            using (var context = new EStoreContext())
            {
                var memberToDelete = await context.Members.FindAsync(memberID);
                if (memberToDelete == null)
                {
                    throw new InvalidOperationException("Member not found.");
                }

                var ordersToDelete = context.Orders.Where(o => o.MemberId == memberID);
                if (await ordersToDelete.AnyAsync())
                {
                    context.Orders.RemoveRange(ordersToDelete);
                }

                context.Members.Remove(memberToDelete);
                await context.SaveChangesAsync();
                return memberID;
            }
        }


        public async Task<Member?> Login(string email, string password)
        {
            using (var context = new EStoreContext())
            {
                return await context.Members.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
            }
        }
    }
}
