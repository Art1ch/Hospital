import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { HomePage } from './components/home-page/home-page';



@NgModule({
  declarations: [
    HomePage
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class HomeModule { }
