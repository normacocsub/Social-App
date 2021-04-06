import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalEditarPublicacionPageRoutingModule } from './modal-editar-publicacion-routing.module';

import { ModalEditarPublicacionPage } from './modal-editar-publicacion.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalEditarPublicacionPageRoutingModule
  ],
  declarations: [ModalEditarPublicacionPage]
})
export class ModalEditarPublicacionPageModule {}
