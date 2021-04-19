import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalContactarPage } from './modal-contactar.page';

const routes: Routes = [
  {
    path: '',
    component: ModalContactarPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalContactarPageRoutingModule {}
