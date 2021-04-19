import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalReaccionesPage } from './modal-reacciones.page';

const routes: Routes = [
  {
    path: '',
    component: ModalReaccionesPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalReaccionesPageRoutingModule {}
