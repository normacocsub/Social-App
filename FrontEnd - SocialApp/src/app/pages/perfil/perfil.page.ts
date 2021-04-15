import { LoginService } from 'src/app/services/login.service';
import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalPage } from '../modal/modal.page';
import { ModalHelpPage } from '../modal-help/modal-help.page';
import { Router } from '@angular/router';
@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.page.html',
  styleUrls: ['./perfil.page.scss'],
})
export class PerfilPage implements OnInit {
  constructor(public modalController: ModalController, private loginService: LoginService, private router: Router) { }

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

  async logout(){
    this.loginService.logout();
    this.router.navigate(['/login']);
  }

}
