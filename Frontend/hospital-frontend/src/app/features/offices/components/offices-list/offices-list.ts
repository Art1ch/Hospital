import { Component, ViewChild } from '@angular/core';
import { GetOfficeModel } from '../../models/get-office-model';
import { CreateOfficeModal } from '../create-office-modal/create-office-modal';
import { OfficeService } from '../../services/office-service';
import { GetOfficesResponse } from '../../models/get-offices-response';
import { CreateOfficeModel } from '../../models/create-office-model';
import { UpdateOfficeModel } from '../../models/update-office-model';
import { UpdateOfficeModal } from '../update-office-modal/update-office-modal';
import { DeleteOfficeModel } from '../../models/delete-office-model';
import { AppFooter } from '../../../../shared/components/app-footer/app-footer';
import { AppHeader } from '../../../../shared/components/app-header/app-header';
import { OfficeCard } from '../office-card/office-card';
import { AppPagination } from '../../../../shared/components/app-pagination/app-pagination';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-offices-list',
  imports: [
    AppFooter,
    AppHeader,
    CreateOfficeModal,
    UpdateOfficeModal,
    OfficeCard,
    AppPagination,
    CommonModule,
    FormsModule
  ],
  standalone: true,
  templateUrl: './offices-list.html',
  styleUrl: './offices-list.scss'
})
export class OfficesList {
  @ViewChild(CreateOfficeModal) createModal!: CreateOfficeModal;
  @ViewChild(UpdateOfficeModal) updateModal!: UpdateOfficeModal;

  offices: GetOfficeModel[] = [];
  page: number = 1;
  pageSize: number = 10;
  totalPages!: number;
  officesOnPage!: number;
  isLoading: boolean = false;

  constructor(private officeService: OfficeService) {
    this.loadOffices();
  }

  loadOffices() {
    this.isLoading = true;

    this.officeService.getOffices(this.page, this.pageSize).subscribe({
      next: (response: GetOfficesResponse) => {
        this.offices = response.result.offices;
        this.totalPages = response.result.totalPages;
        this.officesOnPage = response.result.officesOnPage;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading offices:', error);
        this.isLoading = false;
      }
    });

    console.log(this.offices);
  }

  onOfficeCreated(office: CreateOfficeModel): void {
    this.officeService.createOffice(office).subscribe({
      next: () => {
        this.loadOffices();
      },
      error: (error) => {
        console.error('Error creating office:', error);
      }
    });
  }

  onOfficeEdit(office: GetOfficeModel): void {
    this.updateModal.open(office);
  }

  onOfficeUpdated(updatedOffice: UpdateOfficeModel): void {
    this.officeService.updateOffice(updatedOffice).subscribe({
      next: () => {
        this.loadOffices();
      },
      error: (error) => {
        console.error('Error updating office:', error);
      }
    });
  }

  onOfficeDelete(office: DeleteOfficeModel): void {
    if (office && confirm('Are you sure you want to delete office?')) {
      this.officeService.deleteOffice(office).subscribe({
        next: () => {
          this.loadOffices();
        },
        error: (error) => {
          console.error('Error deleting office:', error);
        }
      });
    }
  }

  goToFirstPage(): void {
    this.page = 1;
    this.loadOffices()
  }

  goToPreviousPage(): void {
    if (this.page > 1) {
      this.page--;
      this.loadOffices();
    }
  }

  goToNextPage(): void {
    if (this.page < this.totalPages!) {
      this.page++;
      this.loadOffices();
    }
  }
}
