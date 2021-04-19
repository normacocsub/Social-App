import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
@Component({
  selector: 'app-modal-security',
  templateUrl: './modal-security.page.html',
  styleUrls: ['./modal-security.page.scss'],
})
export class ModalSecurityPage implements OnInit {

  constructor(public modalController: ModalController) { }

  ngOnInit() {
  }
  closeModal(){
    this.modalController.dismiss();
  }
}
