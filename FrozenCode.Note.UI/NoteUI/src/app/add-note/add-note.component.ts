import { Component, OnInit } from '@angular/core';
import { NoteService } from '../Service/noteservice';

import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NoteDetail, INoteDetail } from '../shared/note-detail.model';
import { FormArray } from '@angular/forms';
import { parse } from 'url';


@Component({
  selector: 'app-add-note',
  templateUrl: './add-note.component.html',
  styleUrls: ['./add-note.component.css']
})
export class AddNoteComponent implements OnInit {

   noteId?: number;
   noteForm = this.fb.group({
       Title: ['', Validators.required],
       Description: [''],
        Content: ['', Validators.required]
   });
    private  note? : INoteDetail;

    constructor(public fb: FormBuilder, public service: NoteService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        
        this.route.paramMap.subscribe(params => {

            this.noteId = Number(params.get("id"));

            if (this.noteId > 0) {
                
                this.service.getNote(this.noteId).then(note =>
                    this.setNoteFormValue(note)
                );                    
            }
          })
        };  

    onSubmit() {
        
        this.service.addNote(this.noteForm.value);
    }

    private setNoteFormValue(objNote) {
        console.log(objNote.Title);
        this.noteForm.patchValue({ Title: objNote.title, Description: objNote.description, Content : objNote.content });
            
    }
}
