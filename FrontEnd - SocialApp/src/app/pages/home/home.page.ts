import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Publicacion } from 'src/app/interfaces/interfaces';
import { LoginService } from 'src/app/services/login.service';
import { ModalPublicacionPage } from '../../pages/modal-publicacion/modal-publicacion.page';
import { PublicacionesService } from '../../services/publicaciones.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  publicaciones: Publicacion[] = [];
  publicacion: Publicacion;
  constructor(private modalController: ModalController,
              public publicacionService: PublicacionesService,
              private service: LoginService) { }

  ngOnInit() {

    this.publicacionService.ConsultaPublicaciones().subscribe(resulta =>{
      this.publicaciones = resulta;
    });


  }


  async crearPublicacion(){
    const modal = await this.modalController.create({
      component: ModalPublicacionPage
    });
    await modal.present();

    const {data} = await modal.onDidDismiss();

 

    this.publicacionService.insertPublicaciones(data).subscribe(result =>{
      
    });
  }

  async crearPublicacionWithImagen(){
    const modal = await this.modalController.create({
      component: ModalPublicacionPage,
      componentProps: {
        image: true
      }
    });
    await modal.present();

    const {data} = await modal.onDidDismiss();

    this.publicacion = data;
    console.log(this.publicacion);

    this.publicacionService.crearPublicacion(this.publicacion).subscribe(result => {
      console.log(result);
    });
    /*
    this.publicacionService.insertPublicaciones(data).subscribe(result =>{
      console.log(result);
    });
    */
  }

}