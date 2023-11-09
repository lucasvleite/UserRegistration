import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../components/users/models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserRegistrationService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private client: HttpClient) { }

  getUser(id: string): Observable<User>
  {
    return this.client.get<User>(this.baseApiUrl + 'api/user/' + id)
  }

  getUsers(): Observable<User[]>
  {
    return this.client.get<User[]>(this.baseApiUrl + 'api/user')
  }

  addUser(addUser: User): Observable<User>
  {
    addUser.id = '00000000-0000-0000-0000-000000000000';
    return this.client.post<User>(this.baseApiUrl + 'api/user', addUser);
  }

  editUser(id: string, editUser: User): Observable<User>
  {
    return this.client.put<User>(this.baseApiUrl + 'api/user/' + id, editUser);
  }

  deleteUser(id: string): Observable<User>
  {
    return this.client.delete<User>(this.baseApiUrl + 'api/user/' + id);
  }
}
