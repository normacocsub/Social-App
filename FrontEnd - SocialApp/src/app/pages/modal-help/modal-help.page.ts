import { ModalController } from '@ionic/angular';
import { Component, OnInit } from '@angular/core';
import { ModalInfoAppPage } from '../modal-info-app/modal-info-app.page';
import { ModalContactarPage } from '../modal-contactar/modal-contactar.page';
@Component({
  selector: 'app-modal-help',
  templateUrl: './modal-help.page.html',
  styleUrls: ['./modal-help.page.scss'],
})
export class ModalHelpPage implements OnInit {

  constructor(public modalController: ModalController) { }

  ngOnInit() {
  }
  async contactar() {
    const modal = await this.modalController.create({
      component: ModalContactarPage,
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }
  closeModal(){
    this.modalController.dismiss();
  }
  async go(){
    const modal = await this.modalController.create({
      component: ModalInfoAppPage,
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }
  terminos(){
    console.log('Hola');
  }

}
