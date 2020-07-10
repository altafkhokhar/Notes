import { Component, OnInit } from '@angular/core';
import { UserService } from '../Service/userservice';

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent implements OnInit {

   
    constructor(public userService: UserService) {

        
    }

    ngOnInit(): void {
        
    }

    logOut() {
        this.userService.logout();
       
    }
    isLoggedIn() {
        return this.userService.isLogged();
    }

}
