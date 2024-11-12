using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMemberRepository
    {
        Task<int> AddMember(Member member);
        Task<int> DeleteMember(int memberId);
        Task<Member?> GetMember(int memberId);
        Task<List<Member>> GetMembers();
        Task<int> UpdateMember(int id,Member member);
        Task<Member?> Login(string email, string password);
    }

    public class MemberRepository : IMemberRepository
    {
        public async Task<int> AddMember(Member member)
        {
            return await MemberDAO.Instance().AddMember(member);
        }

        public async Task<int> DeleteMember(int memberId)
        {
           return await MemberDAO.Instance().DeleteMember(memberId);
        }

        public async Task<Member?> GetMember(int memberId)
        {
            return await MemberDAO.Instance().GetMember(memberId);
        }

        public async Task<List<Member>> GetMembers()
        {
            return await MemberDAO.Instance().GetMembers();
        }

        public async Task<Member?> Login(string email, string password)
        {
            return await MemberDAO.Instance().Login(email, password);
        }

        public async Task<int> UpdateMember(int id,Member member)
        {
            return await MemberDAO.Instance().UpdateMember(id, member);
        }
    }
}
