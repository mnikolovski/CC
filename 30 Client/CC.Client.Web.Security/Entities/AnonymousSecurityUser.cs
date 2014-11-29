using CC.Service.DomainEntities.Users;

namespace CC.Client.Web.Security.Entities
{
    public class AnonymousSecurityUser : SecurityUser
    {
        private static readonly int AnonymousUserId = 0;
        private static readonly string AnonymousUsername = @"Anonymous";
        private static readonly User AnonymousUser;
        public static readonly AnonymousSecurityUser User;

        static AnonymousSecurityUser()
        {
            AnonymousUser = new User();
            AnonymousUser.Id = AnonymousUserId;
            AnonymousUser.Username = AnonymousUsername;
            User = new AnonymousSecurityUser();
        }

        private AnonymousSecurityUser()
            : base(AnonymousUser)
        {
        }
    }
}
