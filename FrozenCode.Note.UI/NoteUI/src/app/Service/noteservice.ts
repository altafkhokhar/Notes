import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRouteSnapshot, Router } from '@angular/router';

import { iApiResult } from "../shared/iApiResult";
import { NoteDetail, INoteDetail, User, IUser, GridNoteDetail } from '../shared/note-detail.model';
import { FormGroup } from '@angular/forms';


@Injectable({
  providedIn: 'root'
})
export class NoteService {
    readonly rootURL = 'http://localhost:50210/api/Notes/';

    readonly userRootURL = 'http://localhost:50210/api/Users/';

    list: GridNoteDetail[];
    result: iApiResult<NoteDetail[]>;
    
    constructor(private httpClient: HttpClient, private router: Router) {

    }

    refreshList() {

        this.httpClient.get(this.rootURL + 'GetAllNotes')
            .toPromise().then
            (res => {
               
                this.list = res as GridNoteDetail[];
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

    searchUsers(searchText: string): Promise<any> {
        return this.httpClient.get<any>(this.userRootURL + 'Search?searchText=' + searchText).toPromise<any>();
    }
     
    getAllusers(): Promise<any> {
        return this.httpClient.get<any>(this.userRootURL + 'GetAllUsers').toPromise<any>();
    }

    updateNoteRightsForUser(userNoteRightDTo) {
        this.httpClient.post(this.rootURL + 'UpdateNoteRights', userNoteRightDTo).toPromise()
            .then(res => {
               
                alert('Record Saved Successfully!');
               
            })
    }

    unShareWithAll(noteId) {
        this.httpClient.post(this.rootURL + 'UnShareWithAll?noteId='+noteId, null).toPromise()
            .then(res => {

                alert('Record Saved Successfully!');

            })
    }
}
