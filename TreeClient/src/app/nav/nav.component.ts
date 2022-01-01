import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {

  }

  login()
  {
    this.authService.login(this.model).subscribe(x => {
      console.log("Zalogowałeś się");
    }, error => {
      console.log('Wystąpił błąd logowania');
    }, () => {
      this.router.navigate(['/tree']);
    });
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout()
  {
    localStorage.removeItem('token');
    console.log('Wylogowano');
    this.router.navigate(['/home']);
  }

}
