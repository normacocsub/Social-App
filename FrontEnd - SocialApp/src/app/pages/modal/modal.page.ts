import { Storage } from '@ionic/storage';
import { Component, OnInit, ViewChild } from '@angular/core';
import { IonToggle, ModalController } from '@ionic/angular';
import { ModalSecurityPage } from '../modal-security/modal-security.page';

import { ModalPrivacidadPage } from '../modal-privacidad/modal-privacidad.page';
import { ConfigurationsService } from 'src/app/services/configurations.service';

import { FingerprintAIO } from '@ionic-native/fingerprint-aio/ngx';

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
  @ViewChild('togleTheme') ionTogleTheme: IonToggle;
  constructor(
    public modalController: ModalController,
    public storage: Storage,
    private configurationService: ConfigurationsService,
    private faio: FingerprintAIO
  ) {}
  ngOnInit() {
    this.opcionInicial = null;
    
  }
  ngAfterViewInit() {

    this.configurationService.verificarTema().subscribe((result:boolean) =>{
      this.ionTogleTheme.checked = result;
    });
    this.configurationService.verificarHuella().subscribe((result) => {
      this.ionToggle.checked = result;
    });

    this.faio.isAvailable().then((value:string) => {
      if(value == "face" || value == "finger" || value == "biometric"){
        this.ionToggle.disabled = false;
      }
    }).catch(error => {this.ionToggle.disabled = true});
  }
  async openModalPrivacidad() {
    const modal = await this.modalController.create({
      component: ModalPrivacidadPage,
      cssClass: 'my-custom-class',
    });
    return await modal.present();
  }
  closeModal() {
    this.modalController.dismiss();
  }
  onToggleColorTheme(event) {
    this.opcion = event.detail.checked;
    this.opcionInicial = event.detail.checked;
    this.configurationService.ActualizarTema(this.opcion);
    if (event.detail.checked) {
      document.body.setAttribute('color-theme', 'dark');
    } else {
      document.body.setAttribute('color-theme', 'ligth');
    }
  }

  onToggleHuella(event) {
    this.opcionHuella = event.detail.checked;
    this.configurationService.ActualizarHuella(this.opcionHuella);
  }

  async openModalSeguridad() {
    const modal = await this.modalController.create({
      component: ModalSecurityPage,
      cssClass: 'my-custom-class',
    });
    return await modal.present();
  }
}
