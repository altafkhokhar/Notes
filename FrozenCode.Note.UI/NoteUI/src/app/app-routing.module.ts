import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { AddNoteComponent } from './add-note/add-note.component'
import { NotesComponent } from './notes/notes.component'


const routes: Routes = [
    { path: 'add-note', component: AddNoteComponent },
    { path: 'list-notes', component: NotesComponent },
    { path: "edit-note/:id", component: AddNoteComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
