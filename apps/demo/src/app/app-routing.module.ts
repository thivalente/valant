import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './pages/home/homecomponent';
import { MazeComponent } from './pages/maze/maze.component';
import { MazeImportComponent } from './pages/maze/import/maze-import.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'maze', component: MazeComponent },
  { path: 'maze/new', component: MazeImportComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }