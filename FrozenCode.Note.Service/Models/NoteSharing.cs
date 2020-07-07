using System;
using System.Collections.Generic;

namespace FrozenCode.Note.Service.Models
{
    public partial class NoteSharing
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public bool Read { get; set; }
        public bool Delete { get; set; }
        public bool Share { get; set; }
        public bool Edit { get; set; }

        public virtual Notes Note { get; set; }
        public virtual Users User { get; set; }
    }
}
