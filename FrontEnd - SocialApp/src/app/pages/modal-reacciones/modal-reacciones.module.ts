import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalReaccionesPageRoutingModule } from './modal-reacciones-routing.module';

import { ModalReaccionesPage } from './modal-reacciones.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalReaccionesPageRoutingModule
  ],
  declarations: [ModalReaccionesPage]
})
export class ModalReaccionesPageModule {}
