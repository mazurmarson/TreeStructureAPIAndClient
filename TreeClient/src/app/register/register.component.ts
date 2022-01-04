
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  model: any ={};
  hero: any={};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register()
  {
    this.authService.register(this.model).subscribe( () => {
      alert('Rejestracja udana');

    }, error => {
      alert(error);
      console.log(error);
    });
  }

  passwordMatch()
  {
    return this.model.password == this.model.confirmPassword;
  }

}
