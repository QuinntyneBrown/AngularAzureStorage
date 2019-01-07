import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { DigitalAssetsModule } from './digital-assets/digital-assets.module';
import { baseUrl } from './core/constants';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,

    DigitalAssetsModule
  ],
  providers: [
    { provide: baseUrl, useValue: "http://localhost:24000/" }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
