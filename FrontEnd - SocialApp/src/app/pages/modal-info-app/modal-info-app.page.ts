import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { AppVersion } from '@ionic-native/app-version/ngx';
@Component({
  selector: 'app-modal-info-app',
  templateUrl: './modal-info-app.page.html',
  styleUrls: ['./modal-info-app.page.scss'],
})
export class ModalInfoAppPage implements OnInit {

  nombre: string = '';
  version: string = '';
  constructor(public modalController: ModalController,
              private appVersion: AppVersion) { }

  ngOnInit() {
    this.appVersion.getAppName().then(val => {
      this.nombre = val;
    })
    this.appVersion.getVersionNumber().then(val => {
      this.version = val;
    })
  }
  closeModal(){
    this.modalController.dismiss();
  }
}
