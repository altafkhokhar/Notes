namespace FrozenCode.Note.Contract.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string CreatedBy { get; set; }
        public string Token { get; set; }
    }
}