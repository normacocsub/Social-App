import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Publicacion } from 'src/app/interfaces/interfaces';

import { ActionSheetController, IonInput, ModalController, ToastController } from '@ionic/angular';
import { ModalEditarPublicacionPage } from 'src/app/pages/modal-editar-publicacion/modal-editar-publicacion.page';
import { PublicacionesService } from 'src/app/services/publicaciones.service';
import { AlertController } from '@ionic/angular';
import { VerPublicacionPage } from 'src/app/pages/ver-publicacion/ver-publicacion.page';

@Component({
  selector: 'app-publicacion',
  templateUrl: './publicacion.component.html',
  styleUrls: ['./publicacion.component.scss'],
})
export class PublicacionComponent implements OnInit {
  @Input() publicacion: Publicacion;
  @Input() comentarios: boolean = false;
  @ViewChild(IonInput) input: IonInput;
  comentario: string = '';
  
  constructor(private actionSheetController: ActionSheetController,
              private modalController: ModalController,
              private PublicacionService: PublicacionesService,
              public toastController: ToastController,
              public alertController: AlertController) { }

  ngOnInit() {}


  ngAfterViewInit(){
    
    if(this.comentarios == true){
      this.input.value = "";
    }
    else{
      if(this.publicacion.comentarios.length == 0){
        this.input.value = "No hay comentarios";
      }else{
        this.input.value = this.publicacion.comentarios[0].idComentario;
      }
      
    }
  }

  comentar(){
    var comentario = {
      idComentario: (this.publicacion.comentarios.length + 1)+'',
      contenidoComentario: this.comentario,
      publicacionId: this.publicacion.idPublicacion
    }
    this.publicacion.comentarios.unshift(comentario);
    this.PublicacionService.publicarComentario(this.publicacion).subscribe(result => {
      this.input.value = "";
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

    this.PublicacionService.updatePublicacion(data).subscribe(result => {
      if(result != null){
        this.presentToast("Se ha editado Correctamente");
      }
    });
    
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
            this.PublicacionService.deletePublicacion(this.publicacion);
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
    const actionSheet = await this.actionSheetController.create({
      cssClass: 'my-custom-class',
      buttons: [ {
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
      }]
    });
    await actionSheet.present();
  }
}
