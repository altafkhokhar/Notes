import { Component, OnInit } from '@angular/core';


import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { UserService } from '../Service/userservice';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

    submitted = false;
    frmSignUp = this.fb.group({
        UserName: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', Validators.required]
    });

    constructor(public fb: FormBuilder, public service: UserService, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

    

    signUp() {
        this.submitted = true;
       
        this.service.signUp(this.frmSignUp.value);
    }

    formControls() { return this.frmSignUp.controls; }

}
