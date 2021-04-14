import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalPage } from '../modal/modal.page';
import { ModalHelpPage } from '../modal-help/modal-help.page';
@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.page.html',
  styleUrls: ['./perfil.page.scss'],
})
export class PerfilPage implements OnInit {
  constructor(public modalController: ModalController) { }

  ngOnInit() {
  }
  async openModal() {
    const modal = await this.modalController.create({
      component: ModalPage,
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }
  async openModalHelp() {
    const modal = await this.modalController.create({
      component: ModalHelpPage,
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }

}
