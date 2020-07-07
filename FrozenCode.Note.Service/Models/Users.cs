using System;
using System.Collections.Generic;

namespace FrozenCode.Note.Service.Models
{
    public partial class Users
    {
        public Users()
        {
            NoteSharing = new HashSet<NoteSharing>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<NoteSharing> NoteSharing { get; set; }
    }
}
