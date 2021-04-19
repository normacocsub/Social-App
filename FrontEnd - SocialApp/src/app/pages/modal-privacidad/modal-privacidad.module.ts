import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalPrivacidadPageRoutingModule } from './modal-privacidad-routing.module';

import { ModalPrivacidadPage } from './modal-privacidad.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalPrivacidadPageRoutingModule
  ],
  declarations: [ModalPrivacidadPage]
})
export class ModalPrivacidadPageModule {}
