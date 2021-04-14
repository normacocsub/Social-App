import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalInfoAppPageRoutingModule } from './modal-info-app-routing.module';

import { ModalInfoAppPage } from './modal-info-app.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalInfoAppPageRoutingModule
  ],
  declarations: [ModalInfoAppPage]
})
export class ModalInfoAppPageModule {}
