import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { IonInput, ModalController } from '@ionic/angular';
import { Comentar } from 'src/app/interfaces/interfaces';

@Component({
  selector: 'app-editar-comentario',
  templateUrl: './editar-comentario.page.html',
  styleUrls: ['./editar-comentario.page.scss'],
})
export class EditarComentarioPage implements OnInit {

  @Input() comentario: Comentar;
  @ViewChild(IonInput) input: IonInput;
  constructor(private modalController: ModalController) { }

  ngOnInit() {
    console.log(this.comentario.idComentario);
  }

  ngAfterViewInit(){ 
    this.input.value = this.comentario.contenidoComentario;
  }

  cancelarModal(){
    this.modalController.dismiss();
  }

  editarModal(){
    this.modalController.dismiss({
      idComentario: this.comentario.idComentario,
      contenidoComentario: this.comentario.contenidoComentario,
      publicacionId: this.comentario.publicacionId,
      usuario: this.comentario.usuario,
      idUsuario: this.comentario.usuario.correo
    });
  }

  getText(event){
    this.comentario.contenidoComentario = event.detail.value;
  }

}
