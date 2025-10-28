import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  constructor(private toastr: ToastrService) {}

  showSuccessNotification(title: string, message?: string) {
    if (message) {
      this.toastr.success(message, title);
    } else {
      this.toastr.success(title);
    }
  }

  showFailedNotification(title: string, message?: string) {
    if (message) {
      this.toastr.error(message, title);
    } else {
      this.toastr.error(title);
    }
  }

  showWarningNotification(title: string, message?: string) {
    if (message) {
      this.toastr.warning(message, title);
    } else {
      this.toastr.warning(title);
    }
  }

  showInfoNotification(title: string, message?: string) {
    if (message) {
      this.toastr.info(message, title);
    } else {
      this.toastr.info(title);
    }
  }
}