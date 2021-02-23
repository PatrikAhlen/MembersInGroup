using System;
using System.DirectoryServices.AccountManagement;
using static System.String;

namespace MembersInGroup
{
    static class Program
    {
        private static string _domainName;
        private static string _groupName;

        static void Main(string[] args)
        {
            _domainName = Empty;
            _groupName = Empty;
                
            while (IsNullOrEmpty(_domainName))
            {
                GetDomainName();
            }

            while (IsNullOrEmpty(_groupName))
            {
                GetGroupName();
            }
            
            GetUsersInAdGroup(_domainName, _groupName);
            Console.ReadLine();
        }

        private static void GetUsersInAdGroup(string domainName, string groupName)
        {
            using var principalContext = new PrincipalContext(ContextType.Domain, domainName);
            var groupPrinciple = new GroupPrincipal(principalContext) {Name = groupName};

            using var searchResult = new PrincipalSearcher(groupPrinciple);
            if (searchResult.FindOne() is GroupPrincipal @group)
            {
                Console.WriteLine($"AD group {@group.Name} has no of members: {@group.Members.Count}");
                        
                foreach (var member in @group.Members)
                {
                    Console.WriteLine(member.Name);
                }    
            }
        }

        private static void GetGroupName()
        {
            Console.WriteLine("Enter group name");
            _groupName = Console.ReadLine()?.Trim();
        }

        private static void GetDomainName()
        {
            Console.WriteLine("Enter Domain name:");
            _domainName = Console.ReadLine()?.Trim();
        }
    }
}
