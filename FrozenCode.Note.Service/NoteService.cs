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

        public bool Edit( CreateNoteDTO note, int userId)
        {
            if (note.Id > 0)
            {
                string title = note.Title.Trim().ToLowerInvariant();

                var dbNote = context.Notes.Where(wh => wh.Title == title && wh.Id != note.Id).FirstOrDefault();
                if (dbNote == null)
                {
                    var noteTobeEdit = (from notes in context.Notes
                                        join noteSharing in context.NoteSharing
                                        on notes.Id equals noteSharing.NoteId
                                        where noteSharing.UserId == userId && noteSharing.Edit
                                        select notes).FirstOrDefault();

                    if (noteTobeEdit != null)
                    {
                        noteTobeEdit.Title = note.Title;
                        noteTobeEdit.Description = note.Description;
                        noteTobeEdit.Content = note.Content;

                        context.SaveChangesAsync();
                        return true;
                    }
                }
            }

            return false;
        }

        public Task<List<GridNoteDetailDTO>> GetAll(int userId)
        {
            //var result =  context.Notes.Select(sel => new NoteDTO {Id = sel.Id, Title = sel.Title, Description = sel.Description, Content = sel.Content }).ToListAsync();
            var result =
                (from notes in context.Notes
                 join noteSharing in context.NoteSharing
                 on notes.Id equals noteSharing.NoteId
                 where (noteSharing.UserId == userId && (noteSharing.Read || noteSharing.Edit || noteSharing.Delete || noteSharing.Share))  
                 select new GridNoteDetailDTO { Id = notes.Id, Title = notes.Title, Content = notes.Content, Description = notes.Description, 
                            CanShare= noteSharing.Edit 
                            , CanRead = noteSharing.Read
                            , CanDelete = noteSharing.Delete
                            , CanEdit = noteSharing.Edit} ).Distinct().ToListAsync();
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

                if (context.Notes.FirstOrDefault(x => x.Title.ToLowerInvariant() == title ) != null)
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


        

        public bool UnShareNoteWithAll(int noteId, int userId)
        {
            var note = context.Notes.Where(wh => wh.Id == noteId && wh.CreatedBy == userId).FirstOrDefault();
            if (note == null)
                return false;

            try
            {
                var noteShared = context.NoteSharing.Where(wh => wh.NoteId == noteId).ToList();
                context.NoteSharing.RemoveRange(noteShared);
                context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool UpdateNoteRightsForUser(UserNoteRightsDTO userNoteRight)
        {
            var noteRights = context.NoteSharing.Where(wh => wh.NoteId == userNoteRight.NoteId && wh.UserId == userNoteRight.UserId).FirstOrDefault();
            if (noteRights != null)
            {

                noteRights.Share = userNoteRight.CanShare;
                noteRights.Delete = userNoteRight.CanDelete;
                noteRights.Edit = userNoteRight.CanEdit;
                noteRights.Read = userNoteRight.CanRead;

                context.SaveChangesAsync();
                return true;
            }
            else
            {
                context.NoteSharing.Add(new NoteSharing
                {
                    NoteId = userNoteRight.NoteId,
                    UserId = userNoteRight.UserId,
                    Delete = userNoteRight.CanDelete,
                    Edit = userNoteRight.CanEdit,
                    Read = userNoteRight.CanRead,
                    Share = userNoteRight.CanShare
                });
                context.SaveChanges();

            }

            return false;

        }
    }
}
