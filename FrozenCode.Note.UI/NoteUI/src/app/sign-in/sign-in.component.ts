import { Component, OnInit } from '@angular/core';

import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { UserService } from '../Service/userservice';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

    submitted = false;
    frmSignIn = this.fb.group({
        UserName: ['', Validators.required],       
        Password: ['', Validators.required]
    });

    constructor(public fb: FormBuilder, public service: UserService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        
    }

    signIn() {
        this.submitted = true;
        this.service.login(this.frmSignIn.value.UserName, this.frmSignIn.value.Password);
        //this.service.login();
    }
}
