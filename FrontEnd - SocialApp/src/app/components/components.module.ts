import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicacionComponent } from './publicacion/publicacion.component';
import { PublicacionesComponent } from './publicaciones/publicaciones.component';
import { IonicModule } from '@ionic/angular';



@NgModule({
  declarations: [
    PublicacionComponent,
    PublicacionesComponent],
  exports: [
    PublicacionComponent,
    PublicacionesComponent 
  ],
  imports: [
    CommonModule,
    IonicModule
  ]
})
export class ComponentsModule { }
