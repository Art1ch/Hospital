import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfficesList } from './components/offices-list/offices-list';
import { OfficeCard } from './components/office-card/office-card';
import { CreateOfficeModal } from './components/create-office-modal/create-office-modal';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { OfficeService } from './services/office-service';
import { UpdateOfficeModal } from './components/update-office-modal/update-office-modal';


@NgModule({
  declarations: [
    OfficesList,
    OfficeCard,
    CreateOfficeModal,
    UpdateOfficeModal,
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
  ],
  providers: [
    OfficeService
  ]
})
export class OfficesModule { }
