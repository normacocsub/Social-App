import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VerPublicacionPageRoutingModule } from './ver-publicacion-routing.module';

import { VerPublicacionPage } from './ver-publicacion.page';
import { ComponentsModule } from 'src/app/components/components.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    VerPublicacionPageRoutingModule, 
    ComponentsModule
  ],
  declarations: [VerPublicacionPage]
})
export class VerPublicacionPageModule {}
