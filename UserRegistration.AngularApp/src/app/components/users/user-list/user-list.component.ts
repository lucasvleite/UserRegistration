import { Component, OnInit } from '@angular/core';
import { UserRegistrationService } from 'src/app/services/user-registration.service';
import { User } from '../models/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private service: UserRegistrationService, private router: Router) { }

  users: User[] = [];

  ngOnInit(): void {
    this.service.getUsers().subscribe({
      next: (users) => {
        this.users = users;
      },
      error: (response) => {
        console.log(response);
      }
    });
  }
  
  deleteUser(id: string) {
    this.service.deleteUser(id).subscribe({
      next: () => {
        this.router.navigate(['users']);
      }
    });
  }

}
