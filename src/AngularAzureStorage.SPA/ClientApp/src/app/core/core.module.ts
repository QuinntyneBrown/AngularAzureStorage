import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { LocalStorageService } from './local-storage.service';
import { LoggerService } from './logger.service';
import { HeaderInterceptor } from './headers.interceptor';
import { HubClient } from './hub-client';
import { HubClientGuard } from './hub-client-guard';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { LoginRedirectService } from './redirect.service';
import { JwtInterceptor } from './jwt.interceptor';
import { ErrorService } from './error.service';
import { OverlayRefProvider } from './overlay-ref-provider';

const providers = [
  AuthGuard,
  AuthService,
  ErrorService,
  HubClient,
  HubClientGuard,
  LocalStorageService,
  LoggerService,
  OverlayRefProvider
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule
  ],
  providers,
  exports: []
})
export class CoreModule {}
