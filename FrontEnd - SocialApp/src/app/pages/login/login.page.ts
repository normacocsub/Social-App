import { AlertController } from '@ionic/angular';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';
import { SpinnerDialog } from '@ionic-native/spinner-dialog/ngx';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  usuario: Usuario = new Usuario();
  formGroup: FormGroup;
  constructor(
    private router: Router,
    public loginService: LoginService,
    public alertController: AlertController,
    private spinnerDialog: SpinnerDialog,
    private formBuilder: FormBuilder,
  ) {
    this.loginService.isLoggedIn().subscribe((result) => {
      if (result) {
        this.router.navigateByUrl('/tabs');
      }
    });
  }

  ngOnInit() {this.buildForm();}

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
    
  }
  async mensaje() {
    const alert = await this.alertController.create({
      header: '',
      subHeader: '',
      message: 'Bienvenido a SocialApp',
    });
    await alert.present();
    let result = await alert.onDidDismiss();
    
  }

  private buildForm(){
    this.usuario = new Usuario();
    this.usuario.correo = '';
    this.usuario.password = '';


    this.formGroup = this.formBuilder.group({
      correo: [this.usuario.correo,[Validators.required, Validators.maxLength(70), Validators.pattern("[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}")]],
      password: [this.usuario.password,[Validators.required, Validators.maxLength(16), Validators.minLength(6)]]
    });
  }


  get control() {
    return this.formGroup.controls;
  }
  onSubmit() {
    if (this.formGroup.invalid) {
      return;
    }
    this.login();
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
   
  }
}
