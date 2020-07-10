namespace FrozenCode.Note.Contract.Entities
{

    public class LoginUserDTO
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }
       
    }
        public class UserDTO : LoginUserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Salt { get; set; }
        public string CreatedBy { get; set; }
        public string Token { get; set; }
    }
}