using FrozenCode.Note.Contract.DTO;
using FrozenCode.Note.Contract.Services;
using FrozenCode.Note.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrozenCode.Note.Service
{
    public class NoteService : INoteService
    {
        private SockoNotesContext context = new SockoNotesContext();
        public NoteService()
        {
            //context = dbContecxt;
        }
        public bool Delete(int noteId, int userId)
        {
            var noteTobeDelete = (from notes in context.Notes
                                 join noteSharing in context.NoteSharing
                                 on notes.Id equals noteSharing.NoteId
                                 where noteSharing.UserId == userId && noteSharing.Delete
                                 select notes).FirstOrDefault();
            if (noteTobeDelete != null)
            {
                var note = context.Notes.Where(wh => wh.Id == noteTobeDelete.Id).FirstOrDefault() ;
                var sharing = context.NoteSharing.Where(wh => wh.NoteId == note.Id).ToList();
                context.NoteSharing.RemoveRange(sharing);
                context.Notes.Remove(note);
                context.SaveChangesAsync();
                return true;
            }
            
            return false;

        }

        public bool Edit(ref CreateNoteDTO note, int userId)
        {
            var noteTobeEdit = (from notes in context.Notes
                                  join noteSharing in context.NoteSharing
                                  on notes.Id equals noteSharing.NoteId
                                  where noteSharing.UserId == userId && noteSharing.Edit
                                  select notes).FirstOrDefault();

            if (noteTobeEdit != null)
            {
                noteTobeEdit.Content = note.Content;
                
                context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public Task<List<NoteDTO>> GetAll()
        {
            var result =  context.Notes.Select(sel => new NoteDTO {Id = sel.Id, Title = sel.Title, Description = sel.Description, Content = sel.Content }).ToListAsync();
            return result;
        }

        public NoteDTO GetNote(int noteId, int userId)
        {
            var noteTobeRead = (from notes in context.Notes
                                join noteSharing in context.NoteSharing
                                on notes.Id equals noteSharing.NoteId
                                where noteSharing.UserId == userId && noteSharing.Read && notes.Id == noteId
                                select notes).FirstOrDefault();

            if (noteTobeRead != null)
            {
                return new NoteDTO { Id = noteTobeRead.Id, Title = noteTobeRead.Title, Description = noteTobeRead.Description, Content = noteTobeRead.Content };
            }

            return null;
        }

        public bool TryCreateNote(ref CreateNoteDTO newNote, int userId)
        {
            try
            {
                string title = newNote.Title.Trim().ToLowerInvariant();

                if (context.Notes.FirstOrDefault(x => x.Title.ToLowerInvariant() == title) != null)
                {
                    return false;
                }

                var dbNote = new Notes() { Title = title, Description = newNote.Description.Trim(), Content = newNote.Content, CreatedBy = userId };
                context.Notes.Add(dbNote);
                context.NoteSharing.Add(new NoteSharing() { NoteId = dbNote.Id, UserId = userId, Delete = true, Edit = true, Read = true, Share = true });
                context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }

       
    }
}
