import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';

import { NoteService } from '../Service/noteservice';
import { User } from '../shared/note-detail.model';

@Component({
  selector: 'app-note-share',
  templateUrl: './note-share.component.html',
    styleUrls: ['./note-share.component.css']
})
export class NoteShareComponent implements OnInit {

    @Input() noteId: string;

    public users: User[];
    constructor(public service: NoteService, public fb: FormBuilder,) { }
    rightForm = this.fb.group({
        CanShare: [''],
        CanEdit: [''],
        CanDelete: [''],
        CanRead: [''],
        UserId:['']
    });

    ngOnInit(): void {
       

  }
    searchUsers(text: string): void {
        if (text.trim()) {
            this.service.searchUsers(text).then<any>(res =>
                this.binUsers(res));
        }
        
    }

    private binUsers(users) {
       
        this.users = users;
    }

    submitRights(userId) {
        
        let canShare = this.rightForm.controls["CanShare"].value === true ? true :false;
        let canEdit = this.rightForm.controls["CanEdit"].value === true ? true : false;
        let canDelete = this.rightForm.controls["CanDelete"].value === true ? true : false;
        let canRead = this.rightForm.controls["CanRead"].value === true ? true : false;

        var noteRightForUser = {
            NoteId: this.noteId,
            UserId: userId,
            CanShare: canShare,
            CanEdit: canEdit,
            CanDelete: canDelete,
            CanRead: canRead       
        };

        this.service.updateNoteRightsForUser(noteRightForUser);       
    }

    dynamicName(can, id) {
        return can + id;
    }    
}
