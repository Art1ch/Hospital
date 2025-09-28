import { NgModule, provideBrowserGlobalErrorListeners, provideZoneChangeDetection, provideZonelessChangeDetection } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import 'zone.js';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { OfficesModule } from './features/offices/offices-module';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './features/home/home-module';
import { HttpClientModule, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

@NgModule({
  declarations: [
    App,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HomeModule,
    SharedModule,
    OfficesModule,
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection(),
    provideHttpClient(withInterceptorsFromDi()),
  ],
  bootstrap: [App]
})
export class AppModule { }
