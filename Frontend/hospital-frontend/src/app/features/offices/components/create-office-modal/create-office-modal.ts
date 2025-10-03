import { Component, EventEmitter, Output } from '@angular/core';
import { OfficeStatus } from '../../models/office-status';
import { CreateOfficeModel } from '../../models/create-office-model';

@Component({
  selector: 'app-create-office-modal',
  standalone: false,
  templateUrl: './create-office-modal.html',
  styleUrls: ['./create-office-modal.scss']
})
export class CreateOfficeModal {
  @Output() officeCreated = new EventEmitter<CreateOfficeModel>();

  isVisible = false;
  newOffice: Partial<CreateOfficeModel> = {};
  selectedImage: File | null = null;
  imagePreview: string | null = null;

  open() {
    this.isVisible = true;
    this.newOffice = {};
    this.selectedImage = null;
    this.imagePreview = null;
  }

  close() {
    this.isVisible = false;
    this.newOffice = {};
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
    const office: CreateOfficeModel = {
      address: this.newOffice.address || '',
      registryPhoneNumber: this.newOffice.registryPhoneNumber || '',
      status: OfficeStatus.Active,
      image: this.selectedImage || undefined
    };

    this.officeCreated.emit(office);
    this.close();
  }

  onCancel() {
    this.close();
  }
}