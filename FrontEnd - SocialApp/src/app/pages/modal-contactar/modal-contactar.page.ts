import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-modal-contactar',
  templateUrl: './modal-contactar.page.html',
  styleUrls: ['./modal-contactar.page.scss'],
})
export class ModalContactarPage implements OnInit {

  constructor(public modalController: ModalController) { }

  ngOnInit() {
  }
  closeModal(){
    this.modalController.dismiss();
  }
  hola(){
    
  }

}
