import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { JwtHelperService } from '@auth0/angular-jwt';
import { from } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseUrl = 'http://localhost:5000/api/auth/';
jwtHelper = new JwtHelperService();
decodedToken: any;
id: string;
role: string;


constructor(private http: HttpClient) {

}


login(model: any)
{
return this.http.post(this.baseUrl + 'login', model)
.pipe(map( (response: any) => {
  const worker = response;
  if(worker)
    {
      localStorage.setItem('token', worker.token);
      this.decodedToken = this.jwtHelper.decodeToken(worker.token);

    }
  } ));
}

register(model: any)
{
  return this.http.post(this.baseUrl + 'register', model);
}

editProfileByWorker(model: any)
{
  return this.http.put(this.baseUrl + 'editWorker', model);
}



loggedIn()
{
  const token = localStorage.getItem('token');

  return !this.jwtHelper.isTokenExpired(token);
}

checkRole()
{
  this.role = this.decodedToken?.role;
  return this.role;
}

getWorkerId():number
{
  this.id = this.decodedToken?.nameid;
  return  Number(this.id);
}

async getWorkerIdAsync():Promise<number>
{
  this.id = await this.decodedToken?.nameid;
  return  Number(this.id);
}

}
