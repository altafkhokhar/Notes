import { Component, OnInit } from '@angular/core';
import { NoteService } from '../Service/noteservice';
import { Router } from '@angular/router';
import { NeedAuthGuard } from '../shared/needAuthGuard';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
    styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {

    constructor(public service: NoteService, private router: Router) { }

    ngOnInit(): void {
        this.service.refreshList();

    }

    deleteNote(id: number): void {
        console.log(id);
        this.service.deleteNote(id);
    }

    goToList(id: number): void {
       
        this.router.navigateByUrl('/edit-note/'+id);

    }

}
