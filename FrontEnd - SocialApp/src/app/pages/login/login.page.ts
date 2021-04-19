import { AlertController } from '@ionic/angular';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';
import { FingerprintAIO } from '@ionic-native/fingerprint-aio/ngx';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  estadoLogin: boolean = false;
  usuario: Usuario = new Usuario();
  opcion2: boolean;
  prueba: any;
  constructor( private router: Router, public loginService: LoginService,
     public alertController: AlertController,
     private faio: FingerprintAIO) {
    this.loginService.isLoggedIn().subscribe(result => {
      if(result){
        this.router.navigateByUrl('/tabs');
      }
    });
  }

  ngOnInit() {
  }
  
  // async mensage(){
  //   this.toast.show(`Bienvenido`, '3000', 'center').subscribe(
  //     toast => {
  //       console.log(toast);
  //     }
  //   );
  // }


  huellaVerifi()
  {
    this.faio.registerBiometricSecret({
      description: "Some biometric description", // optional | Default: null
     secret: "my-super-secret", // mandatory
     invalidateOnEnrollment: true, // optional | Default: false
     disableBackup: true, // (Android Only) | optional | always `true` on Android
      
  })
  .then((result: any) => {this.prueba = result})
  .catch((error: any) => console.log(error));
  }

  async error(){
    const alert = await this.alertController.create({
      header: 'Advertencia',
      subHeader: '',
      message: 'E-mail o contraseña incorrecta',
      buttons: ['OK'],
    });
    await alert.present();
    let result = await alert.onDidDismiss();
    console.log(result);
  }
  async mensaje(){
    const alert = await this.alertController.create({
      header: '',
      subHeader: '',
      message: 'Bienvenido a SocialApp',
    });
    await alert.present();
    let result = await alert.onDidDismiss();
    console.log(result);
  }
  async dactilar(){
    this.huellaVerifi();
    /*
    const alert = await this.alertController.create({
      header: 'Atención',
      subHeader: '',
      message: 'Ingrese su huella para ingresar a SocialApp',
      buttons: ['OK']
    });
    await alert.present();
    let result = await alert.onDidDismiss();
    console.log(result);*/
  }

  login() {
    this.loginService
      .loguearse(this.usuario.correo, this.usuario.password)
      .subscribe((result) => {
        if (result != null) {
          this.mensaje();
          this.router.navigate(['/tabs']);
        }
      },error => this.error());

      console.log(this.loginService.authSubject);
  }
}
