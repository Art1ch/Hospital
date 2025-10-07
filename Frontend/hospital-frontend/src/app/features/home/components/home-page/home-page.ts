import { Component } from '@angular/core';
import { AppHeader } from '../../../../shared/app-header/app-header';
import { AppFooter } from '../../../../shared/app-footer/app-footer';

@Component({
  selector: 'app-home-page',
  imports: [AppHeader, AppFooter],
  standalone: true,
  templateUrl: './home-page.html',
  styleUrl: './home-page.scss'
})
export class HomePage {

}
