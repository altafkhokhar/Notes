using System;
using System.Collections.Generic;
using System.Text;

namespace FrozenCode.Note.Contract.DTO
{
    public class GridNoteDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool CanShare { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool CanRead { get; set; }
    }
}
