import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Publicacion } from 'src/app/models/publicacion';
import { LoginService } from 'src/app/services/login.service';
import { NetworkService } from 'src/app/services/network.service';
import { ModalPublicacionPage } from '../../pages/modal-publicacion/modal-publicacion.page';
import { PublicacionesService } from '../../services/publicaciones.service';
import { ToastController } from '@ionic/angular';


@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  publicaciones: Publicacion[] = [];
  publicacion: Publicacion;
  conectionStatus: boolean = false;
  constructor(private modalController: ModalController,
              public publicacionService: PublicacionesService,
              private service: LoginService,
              private networkService: NetworkService,
              public toastController: ToastController) { }

  ngOnInit() {

    this.networkService.getNetworkStatus().subscribe(result => {
      this.conectionStatus = result;

      if(!this.conectionStatus){
        this.presentToast("No hay conexión a internet ");
      }

    })
    this.publicacionService.ConsultaPublicaciones().subscribe(resulta =>{
      this.publicaciones = resulta;
      console.log(this.publicaciones);
    });

    //this.actualizarListaSignal();
  }

  

  // private actualizarListaSignal(){
  //   this.publicacionService.signalRecived.subscribe((publicacion: Publicacion) => {
  //     this.publicaciones.push(publicacion);
  //   });
  // }


  async presentToast(mensaje: string) {
    const toast = await this.toastController.create({
      message: mensaje,
      duration: 2000
    });
    toast.present();
  }

  async crearPublicacion(){
    /*const modal = await this.modalController.create({
      component: ModalPublicacionPage
    });
    await modal.present();

    const {data} = await modal.onDidDismiss();

 

    this.publicacionService.insertPublicaciones(data).subscribe(result =>{
      
    });
    */
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