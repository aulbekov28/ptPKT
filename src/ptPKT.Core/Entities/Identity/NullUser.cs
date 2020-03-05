namespace ptPKT.Core.Entities.Identity
{
    public sealed class NullUser : AppUser
    {
        public override int Id { get; set; } = -1;
        public override string UserName { get; set; } = "NotAuthorized";
    }
}
