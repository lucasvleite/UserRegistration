import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UserRegistrationService } from 'src/app/services/user-registration.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-password',
  templateUrl: './edit-password.component.html',
  styleUrls: ['./edit-password.component.css']
})
export class EditPasswordComponent implements OnInit {

  passwordDetails: User = { id: '', login: '', name: '', password: '' };

  constructor(private service: UserRegistrationService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.service.getUser(id).subscribe({
            next: (response) => {
              response.password = '';
              this.passwordDetails = response;
            }
          })
        }
      }
    });
  }

  updatePasswordUser() {
    this.service.editUser(this.passwordDetails.id, this.passwordDetails).subscribe({
      next: () => {
        this.router.navigate(['users']);
      }
    })
  }

}
