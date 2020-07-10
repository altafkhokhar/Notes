using FrozenCode.Note.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrozenCode.Note.Contract.Services
{
    public interface INoteService
    {
       
        Task<List<GridNoteDetailDTO>> GetAll(int userId);
        bool TryCreateNote(ref CreateNoteDTO newNote, int userId);

        bool Delete(int noteId, int userId);

        bool Edit(CreateNoteDTO note, int userId);

        NoteDTO GetNote(int noteId, int userId);

        bool UpdateNoteRightsForUser(UserNoteRightsDTO userNoteRight);

        bool UnShareNoteWithAll(int noteId, int userId);
    }
}
