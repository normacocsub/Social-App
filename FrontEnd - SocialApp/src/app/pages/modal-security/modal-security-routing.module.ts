import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalSecurityPage } from './modal-security.page';

const routes: Routes = [
  {
    path: '',
    component: ModalSecurityPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalSecurityPageRoutingModule {}
