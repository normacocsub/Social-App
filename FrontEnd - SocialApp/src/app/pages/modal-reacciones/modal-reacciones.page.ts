import { PublicacionesService } from 'src/app/services/publicaciones.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { IonInput, ModalController } from '@ionic/angular';
import { Publicacion } from 'src/app/models/publicacion';
@Component({
  selector: 'app-modal-reacciones',
  templateUrl: './modal-reacciones.page.html',
  styleUrls: ['./modal-reacciones.page.scss'],
})
export class ModalReaccionesPage implements OnInit {

  @ViewChild(IonInput) input: IonInput;
  @Input() publicacion: Publicacion = new Publicacion();
  usuarioString: string = '';
  publicacionReaccion: any = [];
  personasReaccion: any = [];
  i: number;
  j: number;
  constructor(private publicacionService: PublicacionesService, public modalController: ModalController) { }

  async ngOnInit() {
    this.consultarReaccion();
  }

  closeModal(){
    this.modalController.dismiss();
  }

  consultarReaccion(){
    this.publicacionService.ConsultaPublicaciones().subscribe(result => {
      this.publicacionReaccion = result;
      console.log(this.publicacionReaccion);
    });

    for(this.i; this.i<this.publicacionReaccion.length;this.i++){
        if(this.publicacionReaccion.reacciones[1] == false || this.publicacionReaccion.reacciones[2] == false){
          this.personasReaccion = this.publicacionReaccion;
        }
    }

    if(this.publicacionReaccion){

    }
  }
  
  
}
