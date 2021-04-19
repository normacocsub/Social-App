import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ConsultarPublicacionPageRoutingModule } from './consultar-publicacion-routing.module';

import { ConsultarPublicacionPage } from './consultar-publicacion.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ConsultarPublicacionPageRoutingModule
  ],
  declarations: [ConsultarPublicacionPage]
})
export class ConsultarPublicacionPageModule {}
