import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserRegistrationService } from 'src/app/services/user-registration.service';
import { User } from '../models/user.model';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  userDetails: User = { id: '', login: '', name: '' };

  constructor(private service: UserRegistrationService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.service.getUser(id).subscribe({
            next: (response) => {
              response.password = '';
              this.userDetails = response;
            }
          })
        }
      }
    });
  }

  updateUser() {
    this.service.editUser(this.userDetails.id, this.userDetails).subscribe({
      next: () => {
        this.router.navigate(['users']);
      }
    })
  }

  deleteUser(id: string) {
    this.service.deleteUser(id).subscribe({
      next: () => {
        this.router.navigate(['users']);
      }
    });
  }

}
