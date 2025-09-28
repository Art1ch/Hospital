import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppFooter } from './app-footer/app-footer';
import { AppHeader } from './app-header/app-header';
import { RouterModule } from '@angular/router';
import { AppPagination } from './app-pagination/app-pagination';

@NgModule({
  declarations: [
    AppFooter,
    AppHeader,
    AppPagination
  ],
  exports: [
    AppFooter,
    AppHeader,
    AppPagination
  ],
  imports: [
    CommonModule,
    RouterModule
  ]
})
export class SharedModule { }