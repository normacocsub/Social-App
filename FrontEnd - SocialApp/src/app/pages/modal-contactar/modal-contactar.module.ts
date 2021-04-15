import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalContactarPageRoutingModule } from './modal-contactar-routing.module';

import { ModalContactarPage } from './modal-contactar.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalContactarPageRoutingModule
  ],
  declarations: [ModalContactarPage]
})
export class ModalContactarPageModule {}
