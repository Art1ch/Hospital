import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetOfficeModel } from '../../models/get-office-model';
import { OfficeStatus } from '../../models/office-status';
import { CommonModule } from '@angular/common';
import { EnumToLabelPipe } from '../../../../shared/pipes/enum-to-label-pipe';

@Component({
  standalone: true,
  imports: [CommonModule, EnumToLabelPipe],
  selector: 'app-office-card',
  templateUrl: './office-card.html',
  styleUrl: './office-card.scss'
})
export class OfficeCard {
  @Input() office!: GetOfficeModel;
  @Output() edit = new EventEmitter<GetOfficeModel>(); 
  @Output() delete = new EventEmitter<GetOfficeModel>(); 

  protected readonly OfficeStatus = OfficeStatus;

  onEdit(): void {
    this.edit.emit(this.office); 
  }

  onDelete(): void {
    this.delete.emit(this.office);
  }
}