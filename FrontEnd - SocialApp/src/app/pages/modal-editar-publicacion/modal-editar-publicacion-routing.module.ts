import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalEditarPublicacionPage } from './modal-editar-publicacion.page';

const routes: Routes = [
  {
    path: '',
    component: ModalEditarPublicacionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalEditarPublicacionPageRoutingModule {}
