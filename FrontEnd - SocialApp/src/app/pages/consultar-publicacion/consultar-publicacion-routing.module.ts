import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConsultarPublicacionPage } from './consultar-publicacion.page';

const routes: Routes = [
  {
    path: '',
    component: ConsultarPublicacionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ConsultarPublicacionPageRoutingModule {}
