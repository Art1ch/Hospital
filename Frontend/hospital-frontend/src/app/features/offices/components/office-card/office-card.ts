import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetOfficeModel } from '../../models/get-office-model';
import { OfficeStatus } from '../../models/office-status';

@Component({
  standalone: false,
  selector: 'app-office-card',
  templateUrl: './office-card.html',
  styleUrl: './office-card.scss'
})
export class OfficeCard {
  @Input() office!: GetOfficeModel;
  @Output() edit = new EventEmitter<GetOfficeModel>(); 
  @Output() delete = new EventEmitter<GetOfficeModel>(); 

  getNameOfStatus(status: OfficeStatus): string {
    switch(status) {
      case OfficeStatus.Active: return "Active";
      case OfficeStatus.Inactive: return "Inactive";
      default: return "Unspecified";
    }
  }

  onEdit(): void {
    this.edit.emit(this.office); 
  }

  onDelete(): void {
    this.delete.emit(this.office);
  }
}