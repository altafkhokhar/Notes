import {CanActivate, Router} from '@angular/router';
import {Injectable} from '@angular/core';

import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserService } from '../Service/userservice';


@Injectable()
export class NeedAuthGuard implements CanActivate {

    constructor(private userService: UserService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
      console.log('canActivate');
    const redirectUrl = route['_routerState']['url'];

      if (this.userService.isLogged()) {
        return true;
    }
       
    //this.router.navigateByUrl(
    //  this.router.createUrlTree(
    //    ['/sign-in'], {
    //      queryParams: {
    //        redirectUrl
    //      }
    //    }
    //  )
    //);

    return false;
  }
}
