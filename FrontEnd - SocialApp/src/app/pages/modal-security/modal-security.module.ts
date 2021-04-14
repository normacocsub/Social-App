import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalSecurityPageRoutingModule } from './modal-security-routing.module';

import { ModalSecurityPage } from './modal-security.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalSecurityPageRoutingModule
  ],
  declarations: [ModalSecurityPage]
})
export class ModalSecurityPageModule {}
