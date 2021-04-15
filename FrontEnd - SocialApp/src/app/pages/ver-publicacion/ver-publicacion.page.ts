import { Component, Input, OnInit } from '@angular/core';
import { Publicacion } from 'src/app/models/publicacion';
import { ModalController } from '@ionic/angular';


@Component({
  selector: 'app-ver-publicacion',
  templateUrl: './ver-publicacion.page.html',
  styleUrls: ['./ver-publicacion.page.scss'],
})
export class VerPublicacionPage implements OnInit {

  @Input() publicacion: Publicacion = new Publicacion();
  constructor(private modalController: ModalController) { }

  ngOnInit() {
  }
  
  cancelarModal(){
    this.modalController.dismiss({
      publicacion: this.publicacion.idPublicacion
    });
  }



}
