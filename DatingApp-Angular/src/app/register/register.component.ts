import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';

import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService, private toastr: ToastrService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log("Registration Succesful");
      this.cancel();
    }, error => {
      console.log("Failed to Register");
      this.toastr.error(error.error);
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log("cancel register")
  }

}
