import { Component, EventEmitter, Output } from '@angular/core';
import { UpdateOfficeModel } from '../../models/update-office-model';
import { GetOfficeModel } from '../../models/get-office-model';
import { OfficeStatus } from '../../models/office-status';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-office-modal',
  imports: [CommonModule, FormsModule],
  standalone: true,
  templateUrl: './update-office-modal.html',
  styleUrl: './update-office-modal.scss'
})
export class UpdateOfficeModal {
  @Output() officeUpdated = new EventEmitter<UpdateOfficeModel>();

  isVisible = false;
  editedOffice: Partial<UpdateOfficeModel> = {};
  officeStatus = OfficeStatus;
  selectedImage: File | null = null;
  imagePreview: string | null = null;

  open(office: GetOfficeModel) {
    console.log()
    this.isVisible = true;
    this.editedOffice = { ...office };
    this.editedOffice.oldImageUrl = office.imageUrl;
    this.imagePreview = office.imageUrl || null;
    this.selectedImage = null;
  }

  close() {
    this.isVisible = false;
    this.editedOffice = {};
    this.selectedImage = null;
    this.imagePreview = null;
  }

  onImageSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedImage = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    const office: UpdateOfficeModel = {
      id: this.editedOffice.id || '',
      address: this.editedOffice.address || '',
      registryPhoneNumber: this.editedOffice.registryPhoneNumber || '',
      status: this.editedOffice.status!,
      newImage: this.selectedImage || undefined,
      oldImageUrl: this.editedOffice.oldImageUrl || ''
    };
    this.officeUpdated.emit(office);
    this.close();
  }

  onCancel() {
    this.close();
  }
}