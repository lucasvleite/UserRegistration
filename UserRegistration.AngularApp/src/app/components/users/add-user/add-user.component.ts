import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UserRegistrationService } from 'src/app/services/user-registration.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  addUserRequest: User = {
    id: '',
    name: '',
    login: '',
    password: ''
  }

  constructor(private service: UserRegistrationService, private router: Router) { }

  ngOnInit(): void {
  }

  CreateUser() {
    this.service.addUser(this.addUserRequest).subscribe({
      next: (user) =>
      {
        this.router.navigate(['users']);
      },
      error: (response) =>
      {
        console.log(response);
      }
    });
  }
}
