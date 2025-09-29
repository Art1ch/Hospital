import { Component, EventEmitter, Output } from '@angular/core';
import { UpdateOfficeModel } from '../../models/update-office-model';
import { GetOfficeModel } from '../../models/get-office-model';
import { OfficeStatus } from '../../models/office-status';

@Component({
  selector: 'app-update-office-modal',
  standalone: false,
  templateUrl: './update-office-modal.html',
  styleUrl: './update-office-modal.scss'
})
export class UpdateOfficeModal {
  @Output() officeUpdated = new EventEmitter<UpdateOfficeModel>();

  isVisible = false;
  editedOffice: Partial<UpdateOfficeModel> = {};
  officeStatus = OfficeStatus;

  open(office: GetOfficeModel) {
    this.isVisible = true;
    this.editedOffice = { ...office };
  }

  close() {
    this.isVisible = false;
    this.editedOffice = {};
  }

  onSubmit() {
    const office: UpdateOfficeModel = {
          id: this.editedOffice.id || '',
          address: this.editedOffice.address || '',
          registryPhoneNumber: this.editedOffice.registryPhoneNumber || '',
          status: this.editedOffice.status!,
        };
    this.officeUpdated.emit(office)
    this.close();
  }

  onCancel() {
    this.close();
  }
}
