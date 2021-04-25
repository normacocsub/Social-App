import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalRegistroInfoPageRoutingModule } from './modal-registro-info-routing.module';

import { ModalRegistroInfoPage } from './modal-registro-info.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalRegistroInfoPageRoutingModule
  ],
  declarations: [ModalRegistroInfoPage]
})
export class ModalRegistroInfoPageModule {}
