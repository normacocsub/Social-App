import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarComentarioPageRoutingModule } from './editar-comentario-routing.module';

import { EditarComentarioPage } from './editar-comentario.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarComentarioPageRoutingModule
  ],
  declarations: [EditarComentarioPage]
})
export class EditarComentarioPageModule {}
