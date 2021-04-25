import { Component, OnInit } from '@angular/core';
import {  ModalController } from '@ionic/angular';

@Component({
  selector: 'app-modal-registro-info',
  templateUrl: './modal-registro-info.page.html',
  styleUrls: ['./modal-registro-info.page.scss'],
})
export class ModalRegistroInfoPage implements OnInit {

  constructor(private modalController: ModalController) { }

  ngOnInit() {
  }

  registro(){
    this.modalController.dismiss();
  }

}
