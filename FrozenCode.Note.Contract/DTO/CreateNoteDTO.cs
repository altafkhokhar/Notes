using System;
using System.Collections.Generic;
using System.Text;

namespace FrozenCode.Note.Contract.DTO
{
    public class CreateNoteDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int CreatedBy { get; set; }
        public bool Read { get; set; }
        public bool Delete { get; set; }
        public bool Share { get; set; }
        public bool Edit { get; set; }
    }
}
