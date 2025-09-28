import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-app-pagination',
  standalone: false,
  templateUrl: './app-pagination.html',
  styleUrl: './app-pagination.scss'
})
export class AppPagination {
  @Input() page!: number;
  @Input() pageSize!: number;
  @Input() hasNextPage!: boolean; 
  @Input() isLoading!: boolean;

  @Output() onGoToPrevious = new EventEmitter<void>();
  @Output() onGoToNext = new EventEmitter<void>();
  @Output() onGoToFirst = new EventEmitter<void>(); 

  get canGoPrevious(): boolean {
    return this.page > 1 && !this.isLoading;
  }

  get canGoNext(): boolean {
    return this.hasNextPage && !this.isLoading;
  }

  get canGoFirst(): boolean {
    return this.page > 1 && !this.isLoading;
  }

  goToPrevious() {
    if (this.canGoPrevious) {
      this.onGoToPrevious.emit();
    }
  }

  goToNext() {
    if (this.canGoNext) {
      this.onGoToNext.emit();
    }
  }

  goToFirst() {
    if (this.canGoFirst) {
      this.onGoToFirst.emit();
    }
  }
}
