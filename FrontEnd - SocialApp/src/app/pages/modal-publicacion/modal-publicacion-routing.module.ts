import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalPublicacionPage } from './modal-publicacion.page';

const routes: Routes = [
  {
    path: '',
    component: ModalPublicacionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalPublicacionPageRoutingModule {}
