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

  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.loggedIn();

  }


  login()
  {
    this.authService.login(this.model).subscribe(x => {
      console.log("Zalogowałeś się");

    }, error => {
      alert('Wystąpił błąd logowania');
    }, () => {
      this.router.navigate(['/tree']);
    });
  }

  loggedIn(){
    return this.authService.loggedIn();
  }

  logout()
  {
    localStorage.removeItem('token');
    alert('Wylogowano');
    this.router.navigate(['/home']);
  }





}
