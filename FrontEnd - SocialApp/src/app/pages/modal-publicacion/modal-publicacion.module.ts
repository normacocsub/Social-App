import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalPublicacionPageRoutingModule } from './modal-publicacion-routing.module';

import { ModalPublicacionPage } from './modal-publicacion.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalPublicacionPageRoutingModule
  ],
  declarations: [ModalPublicacionPage]
})
export class ModalPublicacionPageModule {}
