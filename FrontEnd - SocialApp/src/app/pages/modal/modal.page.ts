import { Storage } from '@ionic/storage';
import { Component, OnInit, ViewChild } from '@angular/core';
import { IonToggle, ModalController } from '@ionic/angular';
import { ModalSecurityPage } from '../modal-security/modal-security.page';

import { ModalPrivacidadPage } from '../modal-privacidad/modal-privacidad.page';
import { ConfigurationsService } from 'src/app/services/configurations.service';
@Component({
  selector: 'app-modal',
  templateUrl: './modal.page.html',
  styleUrls: ['./modal.page.scss'],
})
export class ModalPage implements OnInit {
  opcion: boolean;
  opcionInicial: boolean;
  opcionHuella: boolean;
  @ViewChild('toggleHuella') ionToggle: IonToggle;
  constructor(public modalController: ModalController, public storage: Storage,
              private configurationService: ConfigurationsService) {}
  ngOnInit() {
    this.opcionInicial = null; 
    console.log(this.opcionInicial);
  }
  ngAfterViewInit(){
    this.configurationService.verificarHuella().subscribe(result => {
      this.ionToggle.checked = result;
    })
    
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

  onToggleHuella(event){
    this.opcionHuella = event.detail.checked;
    this.configurationService.ActualizarHuella(this.opcionHuella);
  }
  
  async openModalSeguridad(){
    const modal = await this.modalController.create({
      component: ModalSecurityPage,
      cssClass: 'my-custom-class'
    });
    return await modal.present();
  }
}
