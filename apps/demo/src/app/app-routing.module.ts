import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './pages/home/homecomponent';
import { MazeComponent } from './pages/maze/maze.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'maze', component: MazeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }