import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';

import { PublicacionComponent } from './publicacion/publicacion.component';
import { PublicacionesComponent } from './publicaciones/publicaciones.component';
import { IonicModule } from '@ionic/angular';
import  localeEsCo  from '@angular/common/locales/es-CO';

registerLocaleData(localeEsCo, 'es-Co');

@NgModule({
  declarations: [
    PublicacionComponent,
    PublicacionesComponent],
  exports: [
    PublicacionComponent,
    PublicacionesComponent 
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'es-Co'}
  ]
  ,
  imports: [
    CommonModule,
    IonicModule
  ]
})
export class ComponentsModule { }
