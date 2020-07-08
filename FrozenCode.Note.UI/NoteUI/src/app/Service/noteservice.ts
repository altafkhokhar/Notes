import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRouteSnapshot, Router } from '@angular/router';

import { iApiResult } from "../shared/iApiResult";
import { NoteDetail, INoteDetail } from '../shared/note-detail.model';
import { FormGroup } from '@angular/forms';
import { Subscribable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
    readonly rootURL = 'http://localhost:50210/api/Notes/';

    list: NoteDetail[];
    result: iApiResult<NoteDetail[]>;
    
    constructor(private httpClient: HttpClient, private router: Router) {

    }

    refreshList() {

        this.httpClient.get(this.rootURL + 'GetAllNotes')
            .toPromise().then
            (res => {

                //this.result = res as EmployeeDetail[];
                this.list = res as NoteDetail[];
                //console.log(this.list);
            });
    }

    deleteNote(id: number) {
                
        this.httpClient.delete(this.rootURL + 'DeleteNote?noteId=' + id).toPromise()
            .then(res => {
                alert('deleted');
                this.refreshList();
            })
    }

    addNote(noteDetail: INoteDetail) {
        console.log(noteDetail);
        this.httpClient.post(this.rootURL + 'CreateNote', noteDetail).toPromise()
            .then(res => {
                this.refreshList();
                alert('Record Saved Successfully!');
                this.router.navigateByUrl('/list-notes');
            })
    }

    getNote(noteId: number): Promise<NoteDetail> {
        return this.httpClient.get < NoteDetail>(this.rootURL + 'GetNote?noteId=' + noteId).toPromise<NoteDetail>();
    }
}
