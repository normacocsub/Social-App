import { Component, Input, OnInit, ViewChild } from '@angular/core';
import {
  ActionSheetController,
  IonInput,
  IonLabel,
  ModalController,
  ToastController,
} from '@ionic/angular';
import { ModalEditarPublicacionPage } from 'src/app/pages/modal-editar-publicacion/modal-editar-publicacion.page';
import { PublicacionesService } from 'src/app/services/publicaciones.service';
import { AlertController } from '@ionic/angular';
import { VerPublicacionPage } from 'src/app/pages/ver-publicacion/ver-publicacion.page';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';
import { ThisReceiver } from '@angular/compiler';
import { EditarComentarioPage } from 'src/app/pages/editar-comentario/editar-comentario.page';
import { Publicacion } from 'src/app/models/publicacion';
import { Comentario } from 'src/app/models/comentario';
import { Button } from 'selenium-webdriver';
import { Reaccion } from 'src/app/models/reaccion';
import { ModalReaccionesPage } from '../../pages/modal-reacciones/modal-reacciones.page';

@Component({
  selector: 'app-publicacion',
  templateUrl: './publicacion.component.html',
  styleUrls: ['./publicacion.component.scss'],
})
export class PublicacionComponent implements OnInit {
  @Input() publicacion: Publicacion = new Publicacion();
  @Input() comentarios: boolean = false;
  @ViewChild(IonInput) input: IonInput;
  @ViewChild('nombreUsuario') label: IonLabel;
  usuarioString: string = '';
  publicacionEditar: Publicacion;
  comentario: string = '';
  heart: boolean = false;
  like: boolean = false;
  usuario: Usuario = new Usuario();
  comentarioEditar: Comentario;
  reacciones: any = []; 
  

  constructor(
    private actionSheetController: ActionSheetController,
    private modalController: ModalController,
    private PublicacionService: PublicacionesService,
    public toastController: ToastController,
    public alertController: AlertController,
    private service: LoginService
  ) {}

  ngOnInit() {

    console.log(this.calcularDias())
    this.service.getUser().then((result) => {
      result.subscribe((value) => {
        if(value != null)
        {
          this.usuario = value;
          console.log(this.publicacion.reacciones);
          if(this.publicacion.reacciones.length > 0)
          {
            var result = this.publicacion.reacciones.find(p => p.idUsuario == value.correo);
            if(result != null)
            {
              this.heart = result.love;
              this.like = result.like;
            }
          }
        }
      });
    });
    if(this.publicacion.comentarios.length > 0)
    {
      this.usuarioString = this.publicacion.comentarios[0].usuario?.nombres + " " + this.publicacion.comentarios[0].usuario?.apellidos+":"
    }
    //this.actualizarListaSignal();
  }

  async openModalReacciones(){
    const modal = await this.modalController.create({
      component: ModalReaccionesPage,
      componentProps: {
        publicacion: this.publicacion,
      },
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }

  

  calcularDias(){
    var fechaHoy = new Date(Date.now());
    var fecha = new Date(this.publicacion.fecha);
    var day_as_milliseconds = 86400000;
    var horas = ((fechaHoy.getTime()-fecha.getTime()))
    //var diff_in_millisenconds = fechaHoy.getTime() - fecha.getTime();
    //var dias = (Math.round(diff_in_millisenconds / (1000 * 60 * 60 * 24))) + 1;
    return horas;
  }

  menkokora(){
    this.heart = !this.heart;
    this.like = false;

    if(this.like == false && this.heart == false){
      var codigo = this.publicacion.reacciones.find(r => r.idUsuario == this.usuario.correo);
      if(codigo != null){
        this.PublicacionService.eliminarReaccion(codigo.codigo,this.publicacion.idPublicacion)
        .subscribe(result =>{
          this.publicacion = result;
        })
      }
    }else{
      this.mapearReaccion();
    }

    
  }

  mapearReaccion(){
    var reaccion = new Reaccion();
    reaccion.idPublicacion = this.publicacion.idPublicacion;
    reaccion.idUsuario = this.usuario.correo;
    reaccion.like = this.like;
    reaccion.love = this.heart;
    reaccion.usuario = this.usuario;

    this.PublicacionService.editarReaccion(reaccion).subscribe(result => {
      this.publicacion = result;
      
    })
  }

  laik(){
    this.like = !this.like;
    this.heart = false;

    if(this.like == false && this.heart == false){
      var codigo = this.publicacion.reacciones.find(r => r.idUsuario == this.usuario.correo);
      if(codigo != null){
        this.PublicacionService.eliminarReaccion(codigo.codigo,this.publicacion.idPublicacion)
        .subscribe(result =>{
          this.publicacion = result;
        })
      }
    }else{
      this.mapearReaccion();
    }
  }
  
 /* private actualizarListaSignal(){
    this.PublicacionService.signalRecived.subscribe((publicacion: Publicacion) => {
        publicacion = publicacion;
    });
  }
*/
  ngAfterViewInit() {
    if (this.comentarios == true) {
      this.input.value = '';
    } else {
      if (this.publicacion.comentarios.length == 0) {
        this.input.value = 'No hay comentarios';
      } else {
        this.input.value = this.publicacion.comentarios[0].contenidoComentario;
      }
    }
  }

  
  comentar() {
    var comentario = {
      idComentario: '',
      contenidoComentario: this.comentario,
      publicacionId: this.publicacion.idPublicacion,
      usuario: this.usuario,
      idUsuario: this.usuario.correo,
    };
    //this.publicacion.comentarios.unshift(comentario);

    this.PublicacionService.agregarComentario(comentario).subscribe(
      (result) => {
        this.input.value = '';
        console.log(result);
        this.publicacion = result;
      }
    );
  }

  getTextComentario(event) {
    this.comentario = event.detail.value;
  }

  async verPublicacion() {
    const modal = await this.modalController.create({
      component: VerPublicacionPage,
      componentProps: {
        publicacion: this.publicacion,
      },
    });

    await modal.present();

    const { data } = await modal.onDidDismiss();

    this.consultarPublicacion(data);
    
  }

  consultarPublicacion(data: any){
    this.PublicacionService.ConsultaPublicaciones().subscribe(result => {
      this.publicacion = result.find(p => p.idPublicacion == data.publicacion);
      if(this.publicacion.comentarios.length > 0){
        this.input.value =   this.publicacion.comentarios[0].contenidoComentario;
        this.usuarioString = this.publicacion.comentarios[0].usuario?.nombres
        + " " + this.publicacion.comentarios[0].usuario?.apellidos+":";
      }
    });
  }

  async editarPublicacion() {
    const modal = await this.modalController.create({
      component: ModalEditarPublicacionPage,
      componentProps: {
        publica: this.publicacion,
      },
    });
    await modal.present();

    const { data } = await modal.onDidDismiss();

    this.publicacionEditar = data;

    this.PublicacionService.editarPublicacion(this.publicacionEditar).subscribe(
      (result) => {
        if (result != null) {
          console.log('Editado ');
        }
      }
    );
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
          },
        },
        {
          text: 'Eliminar',
          handler: () => {
            //
            this.PublicacionService.eliminarPublicacion(
              this.publicacion
            ).subscribe((result) => {
              console.log(result);
            });
          },
        },
      ],
    });

    await alert.present();
  }

  async presentToast(mensaje) {
    const toast = await this.toastController.create({
      message: mensaje,
      duration: 2000,
    });
    toast.present();
  }

  async lanzarMenu(event) {
    //Buttons
    var Buttons = [
      {
        text: 'Compartir',
        icon: 'share',
        cssClass: 'action-dark',
        handler: () => {},
      },
      {
        text: 'Editar',
        icon: 'create-outline',
        cssClass: 'action-dark',
        handler: () => {
          this.editarPublicacion();
        },
      },
      {
        text: 'Eliminar',
        icon: 'trash-outline',
        cssClass: 'monospace',
        handler: () => {
          this.presentAlertConfirm();
        },
      },
      {
        text: 'Cancel',
        icon: 'close',
        role: 'cancel',
        cssClass: 'monospace',
        handler: () => {
          console.log('Cancel clicked');
        },
      },
    ];
    
    var usuarioValidar = new Usuario();
    (await this.service.getUser()).subscribe((resultado) => {
      usuarioValidar = resultado;
    });

    if (usuarioValidar.correo != this.publicacion.usuario.correo) {
      Buttons.splice(1, 1);
      Buttons.splice(1, 1);
    }
    const actionSheet = await this.actionSheetController.create({
      cssClass: 'my-custom-class',
      buttons: Buttons,
    });
    await actionSheet.present();
  }
  

  async editarComentario(comentario: Comentario) {
    const modal = await this.modalController.create({
      component: EditarComentarioPage,
      componentProps: {
        comentario: comentario,
      },
    });

    await modal.present();

    const { data } = await modal.onDidDismiss();

    this.comentarioEditar = data;

    this.PublicacionService.editarComentario(data).subscribe((result) => {
      this.publicacion = result;
      

    });
  }

  eliminarComentario(comentario: Comentario) {
    this.PublicacionService.eliminarComentario(comentario.idComentario, this.publicacion.idPublicacion)
    .subscribe(result => {
      this.publicacion = result;
    });
  }
  
}
