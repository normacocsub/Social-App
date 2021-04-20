import { AlertController } from '@ionic/angular';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';
import { SpinnerDialog } from '@ionic-native/spinner-dialog/ngx';

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
  constructor(
    private router: Router,
    public loginService: LoginService,
    public alertController: AlertController,
    private spinnerDialog: SpinnerDialog,
  ) {
    this.loginService.isLoggedIn().subscribe((result) => {
      if (result) {
        this.router.navigateByUrl('/tabs');
      }
    });
  }

  ngOnInit() {}

  // async mensage(){
  //   this.toast.show(`Bienvenido`, '3000', 'center').subscribe(
  //     toast => {
  //       console.log(toast);
  //     }
  //   );
  // }

  async error() {
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
  async mensaje() {
    const alert = await this.alertController.create({
      header: '',
      subHeader: '',
      message: 'Bienvenido a SocialApp',
    });
    await alert.present();
    let result = await alert.onDidDismiss();
    console.log(result);
  }

  login() {
    this.spinnerDialog.show(null, "Cargando");
    this.loginService
      .loguearse(this.usuario.correo, this.usuario.password)
      .subscribe(
        (result) => {
          if (result != null) {
            this.spinnerDialog.hide();
            this.mensaje();
            this.router.navigate(['/tabs']);
          }
        },
        (error) => this.error()
      );
    this.spinnerDialog.hide();
    console.log(this.loginService.authSubject);
  }
}
