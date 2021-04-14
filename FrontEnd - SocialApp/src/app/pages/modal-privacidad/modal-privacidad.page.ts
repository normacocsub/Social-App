import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
@Component({
  selector: 'app-modal-privacidad',
  templateUrl: './modal-privacidad.page.html',
  styleUrls: ['./modal-privacidad.page.scss'],
})
export class ModalPrivacidadPage implements OnInit {

  constructor(public modalController: ModalController) { }

  ngOnInit() {
  }
  closeModal(){
    this.modalController.dismiss();
  }
}
