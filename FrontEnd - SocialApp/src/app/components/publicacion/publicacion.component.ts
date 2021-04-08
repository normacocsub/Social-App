import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Comentar, Publicacion } from 'src/app/interfaces/interfaces';

import { ActionSheetController, IonInput, ModalController, ToastController } from '@ionic/angular';
import { ModalEditarPublicacionPage } from 'src/app/pages/modal-editar-publicacion/modal-editar-publicacion.page';
import { PublicacionesService } from 'src/app/services/publicaciones.service';
import { AlertController } from '@ionic/angular';
import { VerPublicacionPage } from 'src/app/pages/ver-publicacion/ver-publicacion.page';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';
import { ThisReceiver } from '@angular/compiler';
import { EditarComentarioPage } from 'src/app/pages/editar-comentario/editar-comentario.page';


@Component({
  selector: 'app-publicacion',
  templateUrl: './publicacion.component.html',
  styleUrls: ['./publicacion.component.scss'],
})
export class PublicacionComponent implements OnInit {
  @Input() publicacion: Publicacion;
  @Input() comentarios: boolean = false;
  @ViewChild(IonInput) input: IonInput;
  publicacionEditar: Publicacion;
  comentario: string = '';
  usuario: Usuario = new Usuario();
  comentarioEditar: Comentar;
  
  constructor(private actionSheetController: ActionSheetController,
              private modalController: ModalController,
              private PublicacionService: PublicacionesService,
              public toastController: ToastController,
              public alertController: AlertController,
              private service: LoginService) { }

  ngOnInit() {
    this.service.getUser().then(result => {
    result.subscribe(value => {
      this.usuario = value;
    })
  });}


  ngAfterViewInit(){
    
    if(this.comentarios == true){
      this.input.value = "";
    }
    else{
      if(this.publicacion.comentarios.length == 0){
        this.input.value = "No hay comentarios";
      }else{
        this.input.value = this.publicacion.comentarios[0].contenidoComentario;
      }
      
    }
  }

  comentar(){
    
    var comentario = {
      idComentario: '',
      contenidoComentario: this.comentario,
      publicacionId: this.publicacion.idPublicacion,
      usuario: this.usuario,
      idUsuario: this.usuario.correo
    }
    this.publicacion.comentarios.unshift(comentario);

    this.PublicacionService.agregarComentario(this.publicacion).subscribe(result => {
      this.input.value = "";
      console.log(this.publicacion);
    });
  }

  getTextComentario(event){
    this.comentario = event.detail.value;
  }

  async verPublicacion(){
    const modal = await this.modalController.create({
      component: VerPublicacionPage,
      componentProps: {
        publicacion: this.publicacion
      }
    });

    await modal.present();

    const {data} = await modal.onDidDismiss();
  }  
  
  async editarPublicacion(){
    const modal = await this.modalController.create({
      component: ModalEditarPublicacionPage,
      componentProps: {
        publica: this.publicacion
      }
    });
    await modal.present();

    const {data} = await modal.onDidDismiss();

    this.publicacionEditar = data;


    this.PublicacionService.editarPublicacion(this.publicacionEditar).subscribe(result => {
      if(result != null){
        console.log("Editado ");
      }
    })

    
  }

  async presentAlertConfirm() {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Confirm!',
      message: 'Seguro Que desea Eliminar la Publicacion?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Eliminar',
          handler: () => {
            //
            this.PublicacionService.eliminarPublicacion(this.publicacion).subscribe(result => {
              console.log(result);
            })
          }
        }
      ]
    });

    await alert.present();
  }

  async presentToast(mensaje) {
    const toast = await this.toastController.create({
      message: mensaje,
      duration: 2000
    });
    toast.present();
  }



  async lanzarMenu(event){

    //Buttons
    var Buttons = [{
      text: 'Compartir',
      icon: 'share',
      cssClass: 'action-dark',
      handler: () => {
        
      }
    },    
    {
      text: 'Editar',
      icon: 'create-outline',
      cssClass: 'action-dark',
      handler: () => {
        this.editarPublicacion();
      }
    },
    {
      text: 'Eliminar',
      icon: 'trash-outline',
      cssClass: 'action-dark',
      handler: () => {
        this.presentAlertConfirm();
      }
    }
    ,  {
      text: 'Cancel',
      icon: 'close',
      role: 'cancel',
      cssClass: 'action-dark',
      handler: () => {
        console.log('Cancel clicked');
      }
    }];

    var usuarioValidar = new Usuario();
    (await this.service.getUser()).subscribe(resultado => {
      usuarioValidar = resultado;
    });

    if(usuarioValidar.correo != this.publicacion.usuario.correo){
      Buttons.splice(1, 1);
      Buttons.splice(1, 1);
    }
    const actionSheet = await this.actionSheetController.create({
      cssClass: 'my-custom-class',
      buttons: Buttons
    });
    await actionSheet.present();
  }

  async editarComentario(comentario: Comentar){

    const modal = await this.modalController.create({
      component: EditarComentarioPage,
      componentProps: {
        comentario: comentario
      }
    });

    await modal.present();

    const {data} = await modal.onDidDismiss();

    this.comentarioEditar = data;


    this.PublicacionService.editarComentario(data).subscribe(result => {
      console.log(result);
    })
  }

  eliminarComentario(comentario: Comentar){

  }
}
