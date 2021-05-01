import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';

import { ModalController } from '@ionic/angular';
import { ModalRegistroInfoPage } from '../modal-registro-info/modal-registro-info.page';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';

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
  formGroup: FormGroup;
  constructor(
    private usuarioService: LoginService,
    private modalController: ModalController,
    private router: Router,
    private spinner: SpinnerDialog,
    private camera: Camera,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.usuario = new Usuario();
    this.buildForm();
  }

  private buildForm(){
    this.usuario = new Usuario();
    this.usuario.nombres = '';
    this.usuario.apellidos = '';
    this.usuario.correo = '';
    this.usuario.imagePerfil = '';
    this.usuario.password = '';
    this.usuario.sexo = '';

    this.formGroup = this.formBuilder.group({
      nombres: [this.usuario.nombres, [Validators.required, Validators.minLength(1), Validators.maxLength(60)]],
      apellidos: [this.usuario.apellidos, [Validators.required, Validators.minLength(1), Validators.maxLength(60)]],
      correo: [this.usuario.correo, [Validators.required, Validators.maxLength(70), Validators.pattern("[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}")]],
      imagePerfil: [this.usuario.imagePerfil],
      password: [this.usuario.password, [Validators.required, Validators.maxLength(16), Validators.minLength(6)]],
      sexo: [this.usuario.sexo, [Validators.required, this.validarSexo]],
    });
  }


  private validarSexo(control: AbstractControl){
    const sexo = control.value;
    if (sexo.toLocaleUpperCase() !== 'MASCULINO' && sexo.toLocaleUpperCase() !== 'FEMENINO') {
      return { validSexo: true, messageSexo: 'Sexo No Valido' };
    }
    return null;
  }

  get control() {
    return this.formGroup.controls;
  }
  onSubmit() {
    if (this.formGroup.invalid) {
      return;
    }
    this.registrar();
  }


  agregarFotoPerfil(){
    this.Gallery();
    if(this.tardarEnvio == true){
      //this.imagen = this.usuario.imagePerfil;
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
      this.imagen = base64Image;
      this.avisarSeleccionImg = true;
     }, (err) => {
      // Handle error
     });
  }

  registrar() {
    this.usuario = this.formGroup.value;
    this.usuario.imagePerfil = this.imagen;
    console.log(this.usuario);
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
