import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { LoggingService } from './logging/logging.service';
import { MazeService } from './pages/maze/maze.service';
import { ValantDemoApiClient } from './api-client/api-client';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/homecomponent';
import { MazeComponent } from './pages/maze/maze.component';

import { environment } from '../environments/environment';

export function getBaseUrl(): string {
  return environment.baseUrl;
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MazeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    LoggingService,
    MazeService,
    ValantDemoApiClient.Client,
    { provide: ValantDemoApiClient.API_BASE_URL, useFactory: getBaseUrl },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
