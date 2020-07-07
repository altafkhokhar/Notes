using System;
using System.Collections.Generic;

namespace FrozenCode.Note.Service.Models
{
    public partial class Notes
    {
        public Notes()
        {
            NoteSharing = new HashSet<NoteSharing>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<NoteSharing> NoteSharing { get; set; }
    }
}
