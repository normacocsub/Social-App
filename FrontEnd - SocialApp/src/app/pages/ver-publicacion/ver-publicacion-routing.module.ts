import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VerPublicacionPage } from './ver-publicacion.page';

const routes: Routes = [
  {
    path: '',
    component: VerPublicacionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VerPublicacionPageRoutingModule {}
