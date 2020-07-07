using FrozenCode.Note.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrozenCode.Note.Contract.Services
{
    public interface INoteService
    {
       
        Task<List<NoteDTO>> GetAll();
        bool TryCreateNote(ref CreateNoteDTO newNote, int userId);

        bool Delete(int noteId, int userId);

        bool Edit(ref CreateNoteDTO note, int userId);

        NoteDTO GetNote(int noteId,int userId);
    }
}
