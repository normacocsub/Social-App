import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  estadoLogin: boolean = false;
  usuario: Usuario = new Usuario();

  constructor(private router: Router, public loginService: LoginService) {
    this.loginService.isLoggedIn().subscribe(result => {
      if(result){
        this.router.navigateByUrl('/tabs');
      }
    });
  }

  ngOnInit() {
  }

  login() {
    this.loginService
      .loguearse(this.usuario.correo, this.usuario.password)
      .subscribe((result) => {
        if (result != null) {
          this.router.navigate(['/tabs']);
        }
      });
    
      console.log(this.loginService.authSubject);
    
  }
}
