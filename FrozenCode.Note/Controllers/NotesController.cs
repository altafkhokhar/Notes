
using FrozenCode.Note.Contract.DTO;
using FrozenCode.Note.Contract.Services;
using FrozenCode.Note.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Helpers;

namespace FrozenCode.Note.API.Controllers
{
    
    public class NotesController : BaseController
    {
        private INoteService _noteService;
        private readonly AppSettings _appSettings;  

        public NotesController( IOptions<AppSettings> appSettings)
        {
            _noteService = new NoteService();
            _appSettings = appSettings.Value;
        }

        //[AllowAnonymous]
        [HttpPost]
        [Route("CreateNote")]//  api/notes/CreateNote

        public IActionResult CreateNote([FromBody]CreateNoteDTO newNote)
        {
            string message = string.Empty, operationMessage = string.Empty; bool isSucceeded = false;

            if (newNote.Id > 0)
            {
                isSucceeded = _noteService.Edit(newNote, GetUserId());
                operationMessage = "Can not update note!";
            }
            else
            {
                isSucceeded = _noteService.TryCreateNote(ref newNote, GetUserId());
                operationMessage = "Can not create a note!";
            }
            if (!isSucceeded)
                return BadRequest(new { message = operationMessage});

            return Ok(true);
        }


        //[AllowAnonymous]
        [HttpPost]
        [Route("EditNote")]//  api/notes/EditNote
        public IActionResult EditNote([FromBody]CreateNoteDTO editNote)
        {
            string message = string.Empty;

            var isUpdated = _noteService.Edit(editNote, GetUserId());

            if (!isUpdated)
                return BadRequest(new { message = "Cannot update a Note!" });

            return Ok(true);
        }

        //[AllowAnonymous]
        [HttpDelete]
        [Route("DeleteNote")]//  api/notes/DeleteNote
        public IActionResult DeleteNote([FromQuery]int noteId)
        {
            string message = string.Empty;

            var isDeleted = _noteService.Delete(noteId, GetUserId());

            if(!isDeleted)
                return BadRequest(new { message = "Cannot delete a Note!" });

            return Ok(true);
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("GetNote")]//  api/notes/GetNote
        public IActionResult GetNote([FromQuery]int noteId)
        {
            string message = string.Empty;

            var readNote = _noteService.GetNote(noteId, GetUserId());

            if (readNote == null)
                return BadRequest(new { message = "Invalid note!" });

            return Ok(readNote);
        }

       // [AllowAnonymous]
        [HttpGet]
        [Route("GetAllNotes")]//  api/notes/GetNote
        public IActionResult GetAllNotes()
        {
            string message = string.Empty;

            var notes = _noteService.GetAll(GetUserId()).Result;


            return Ok(notes);
        }

        //[AllowAnonymous] //todo
        [HttpPost]
        [Route("UpdateNoteRights")]//  api/notes/GetNote
        public IActionResult UpdateNoteRights([FromBody]UserNoteRightsDTO userNoteRightDTo)
        {
            string message = string.Empty;

            var result = _noteService.UpdateNoteRightsForUser(userNoteRightDTo);


            return Ok(result);
        }

        //[AllowAnonymous] //todo
        [HttpPost]
        [Route("UnShareWithAll")]//  api/notes/GetNote
        public IActionResult UnShareWithAll([FromQuery]int noteId)
        {
            string message = string.Empty;

            var result = _noteService.UnShareNoteWithAll(noteId, GetUserId());


            return Ok(result);
        }
    }
}