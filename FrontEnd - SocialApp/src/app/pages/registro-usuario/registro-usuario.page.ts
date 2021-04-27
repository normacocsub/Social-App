import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';

import { ModalController } from '@ionic/angular';
import { ModalRegistroInfoPage } from '../modal-registro-info/modal-registro-info.page';
import { Router } from '@angular/router';

import { SpinnerDialog } from '@ionic-native/spinner-dialog/ngx';
import { Camera, CameraOptions } from '@ionic-native/camera/ngx';

@Component({
  selector: 'app-registro-usuario',
  templateUrl: './registro-usuario.page.html',
  styleUrls: ['./registro-usuario.page.scss'],
})
export class RegistroUsuarioPage implements OnInit {
  usuario: Usuario;
  imagen: string = '';
  avisarSeleccionImg: boolean = false;
  tardarEnvio: boolean = false;
  constructor(
    private usuarioService: LoginService,
    private modalController: ModalController,
    private router: Router,
    private spinner: SpinnerDialog,
    private loginService: LoginService,
    private camera: Camera,
  ) {
    this.loginService.getUser().then((value) => {
      value.subscribe((result:Usuario) => {
        this.loginService.buscarUsuario(result.correo).subscribe(resultado =>{
          if(resultado != null){
            this.usuario = resultado;
            if(result.imagePerfil = ''){
              this.imagen = 'assets/descarga.jpg';
            }
            else{
              this.imagen = result.imagePerfil;
            }
          }
        })
        
        
      })
    })
  }

  ngOnInit() {
    this.usuario = new Usuario();
  }
  agregarFotoPerfil(){
    this.Gallery();
    if(this.tardarEnvio == true){
      this.imagen = this.usuario.imagePerfil;
    }
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
      this.avisarSeleccionImg = true;
      this.loginService.editarImagen(this.usuario).subscribe(result => {
        this.usuario = result;
        this.spinner.hide();
      });

     }, (err) => {
      // Handle error
     });
  }

  registrar() {
    this.spinner.show(null, "Cargando", true);
    this.usuarioService.post(this.usuario).subscribe(async (result) => {
      if (result != null) {
        this.spinner.hide();
        const modal = await this.modalController.create({
          component: ModalRegistroInfoPage,
        });
        await modal.present();

        this.tardarEnvio = true;
    
        await modal.onDidDismiss();

        this.router.navigate(['/']);

      }
    });
  }
}
