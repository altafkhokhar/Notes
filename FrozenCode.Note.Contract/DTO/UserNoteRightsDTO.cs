using System;
using System.Collections.Generic;
using System.Text;

namespace FrozenCode.Note.Contract.DTO
{
    public class UserNoteRightsDTO
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }

        public bool CanShare { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool CanRead { get; set; }
    }
}
