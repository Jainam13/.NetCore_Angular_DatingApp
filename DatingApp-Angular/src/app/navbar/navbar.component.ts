import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log("login succesful");
      this.router.navigateByUrl('/members');
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }

}
