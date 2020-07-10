import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { AddNoteComponent } from './add-note/add-note.component'
import { NotesComponent } from './notes/notes.component'
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { NeedAuthGuard } from './shared/needAuthGuard';
import { NoteShareComponent } from './note-share/note-share.component';


const routes: Routes = [
    {
        path: 'add-note', component: AddNoteComponent, canActivate: [NeedAuthGuard] }, 
    { path: 'list-notes', component: NotesComponent, canActivate: [NeedAuthGuard] },
    { path: "edit-note/:id", component: AddNoteComponent , canActivate: [NeedAuthGuard] },
    { path: "sign-up", component: SignUpComponent },
    { path: "sign-in", component: SignInComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
