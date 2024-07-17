import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { LoggingService } from './logging/logging.service';
import { MazeService } from './pages/maze/maze.service';
import { ValantDemoApiClient } from './api-client/api-client';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/homecomponent';
import { MazeComponent } from './pages/maze/maze.component';
import { MazeImportComponent } from './pages/maze/import/maze-import.component';

import { environment } from '../environments/environment';

export function getBaseUrl(): string {
  return environment.baseUrl;
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MazeComponent,
    MazeImportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
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
