import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalHelpPageRoutingModule } from './modal-help-routing.module';

import { ModalHelpPage } from './modal-help.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalHelpPageRoutingModule
  ],
  declarations: [ModalHelpPage]
})
export class ModalHelpPageModule {}
