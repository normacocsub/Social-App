import { Storage } from '@ionic/storage';
import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalSecurityPage } from '../modal-security/modal-security.page';

import { ModalPrivacidadPage } from '../modal-privacidad/modal-privacidad.page';
@Component({
  selector: 'app-modal',
  templateUrl: './modal.page.html',
  styleUrls: ['./modal.page.scss'],
})
export class ModalPage implements OnInit {
  opcion: boolean;
  opcionInicial: boolean;
  constructor(public modalController: ModalController, public storage: Storage) {}
  ngOnInit() {
    this.opcionInicial = null; 
    console.log(this.opcionInicial);
  }
  async openModalPrivacidad() {
    const modal = await this.modalController.create({
      component: ModalPrivacidadPage,
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }
  closeModal(){
    this.modalController.dismiss();
  }
  onToggleColorTheme(event){
    console.log(event.detail.checked)
    this.opcion = event.detail.checked;
    this.opcionInicial = event.detail.checked;
    if(event.detail.checked){
      document.body.setAttribute('color-theme','dark');
    }else{
      document.body.setAttribute('color-theme','ligth');
    }
  }
  async openModalSeguridad(){
    const modal = await this.modalController.create({
      component: ModalSecurityPage,
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }
}
