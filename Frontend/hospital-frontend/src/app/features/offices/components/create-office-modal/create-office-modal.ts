import { Component, EventEmitter, Output } from '@angular/core';
import { OfficeStatus } from '../../models/office-status';
import { CreateOfficeModel } from '../../models/create-office-model';

@Component({
  selector: 'app-create-office-modal',
  standalone: false,
  templateUrl: './create-office-modal.html',
  styleUrl: './create-office-modal.scss'
})
export class CreateOfficeModal {
  @Output() officeCreated = new EventEmitter<CreateOfficeModel>();

  isVisible = false;
  newOffice: Partial<CreateOfficeModel> = {};

  open() {
    this.isVisible = true;
    this.newOffice = {};
  }

  close() {
    this.isVisible = false;
    this.newOffice = {};
  }

  onSubmit() {
    const office: CreateOfficeModel = {
      address: this.newOffice.address || '',
      registryPhoneNumber: this.newOffice.registryPhoneNumber || '',
      status: OfficeStatus.Active,
    };

    this.officeCreated.emit(office);
    this.close();
  }

  onCancel() {
    this.close();
  }
}
