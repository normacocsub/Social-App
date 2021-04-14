import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
@Component({
  selector: 'app-modal-info-app',
  templateUrl: './modal-info-app.page.html',
  styleUrls: ['./modal-info-app.page.scss'],
})
export class ModalInfoAppPage implements OnInit {

  constructor(public modalController: ModalController) { }

  ngOnInit() {
  }
  closeModal(){
    this.modalController.dismiss();
  }
}
