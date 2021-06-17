import { LoginService } from 'src/app/services/login.service';
import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalPage } from '../modal/modal.page';
import { ModalHelpPage } from '../modal-help/modal-help.page';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';

import { Camera, CameraOptions } from '@ionic-native/camera/ngx';
import { SpinnerDialog } from '@ionic-native/spinner-dialog/ngx';
@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.page.html',
  styleUrls: ['./perfil.page.scss'],
})
export class PerfilPage implements OnInit {
  usuario: Usuario = new Usuario();
  imagen: string = '';
  constructor(
    public modalController: ModalController,
    private loginService: LoginService,
    private router: Router,
    private camera: Camera,
    private spinner: SpinnerDialog,
  ) {
    
    this.loginService.getUser().then((value) => {
      value.subscribe((result:Usuario) => {
        this.loginService.buscarUsuario(result.correo).then(value => value.subscribe(resultado => {
          if(resultado != null){
            this.usuario = resultado;
            if(result.imagePerfil = ''){
              this.imagen = 'assets/descarga.jpg';
            }
            else{
              this.imagen = result.imagePerfil;
            }
          }
        }));
        
        
      })
    });
  }

  ngOnInit() {}
  async openModal() {
    const modal = await this.modalController.create({
      component: ModalPage,
      cssClass: 'my-custom-class',
    });
    return await modal.present();
  }
  hola() {}
  async openModalHelp() {
    const modal = await this.modalController.create({
      component: ModalHelpPage,
      cssClass: 'my-custom-class',
    });
    return await modal.present();
  }

  cambiarFotoPerfil(){
    this.Gallery();
    this.imagen = this.usuario.imagePerfil;
  }

  async logout() {
    this.loginService.logout();
    this.router.navigate(['/login']);
  }

  Gallery(){
    
    const options: CameraOptions = {
      quality: 60,
      destinationType: this.camera.DestinationType.DATA_URL,
      encodingType: this.camera.EncodingType.JPEG,
      mediaType: this.camera.MediaType.PICTURE,
      correctOrientation: true,
      sourceType: this.camera.PictureSourceType.PHOTOLIBRARY
    }

    this.procesarImagen(options);
  }

  procesarImagen(options: CameraOptions){
    this.camera.getPicture(options).then((imageData) => {
      // imageData is either a base64 encoded string or a file URI
      // If it's base64 (DATA_URL):
      //let win: any = window;
      //const img = win.Ionic.WebView.convertFileSrc( imageData );
      let base64Image = 'data:image/jpeg;base64,' + imageData;
      this.usuario.imagePerfil = base64Image;
      this.spinner.show(null, "Actualizando Imagen", false);
      this.loginService.editarImagen(this.usuario).then(value => value.subscribe(result => {
        this.usuario = result;
        this.spinner.hide();
      }));

     }, (err) => {
      // Handle error
     });
  }
}
