import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { NotesComponent } from './notes/notes.component';
import { AddNoteComponent } from './add-note/add-note.component';
import { ToolBarComponent } from './tool-bar/tool-bar.component';
import { NoteShareComponent } from './note-share/note-share.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ErrorInterceptor } from './shared/errorInterceptor';

import { NeedAuthGuard } from './shared/needAuthGuard';
import { NavigationStart } from '@angular/router';





@NgModule({
  declarations: [
    AppComponent,
    NotesComponent,
    AddNoteComponent,
    ToolBarComponent,
    NoteShareComponent,
    SignInComponent,
        SignUpComponent
        
  ],
  imports: [
    BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      ReactiveFormsModule
    ],
    providers: [{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }, NeedAuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
